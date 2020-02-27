
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.context;
using System.Collections.Generic;
using cReg_WebApp.Controllers.Logic;
using cReg_WebApp.Models.ViewModels;

namespace cReg_WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;
        private Functions functions;

        public CourseController(DataContext context)
        {
            _context = context;
            functions = new Functions(context);
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult>Details(int? sid , int? cid)
        {
            if(sid!=null)
            {
                var stu = await _context.Students.FirstOrDefaultAsync(s => s.studentId == sid);
                ViewData["studentId"] = sid;
                ViewData["Name"] = stu.name;
                if (cid == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .FirstOrDefaultAsync(m => m.courseId == cid);
                if (course == null)
                {
                    return NotFound();
                }

                return View(course);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegisterDetails(int? sid, int? cid)
        {
            if (sid != null)
            {
                var stu = await _context.Students.FirstOrDefaultAsync(s => s.studentId == sid);
                if (cid == null)
                {
                    return NotFound();
                }

                CourseViewModel thisModel = new CourseViewModel(cid.GetValueOrDefault(), _context);
                thisModel.user = stu;
                if (thisModel == null)
                {
                    return NotFound();
                }

                return View(thisModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> Register(int? sid, int? cid)
        {
            if(sid!=null&&cid!=null)
            {
                if(await functions.registrationVerifierAsync(sid.GetValueOrDefault(), cid.GetValueOrDefault()))
                {
                    _context.Enrolled.Add(new Enrolled {  courseId = cid.GetValueOrDefault(), studentId = sid.GetValueOrDefault(), completed = false, grade = null, rating = null, comment = null });
                    await _context.SaveChangesAsync();
                    ViewBag.message = "<scipt>alert('Success Registration');</script>";
                }
                else
                {
                    ViewBag.message = "<scipt>alert('Failed Registration');</script>";
                }
                return RedirectToAction("Register","Home", new { id = sid });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public IActionResult DropDetails(int? eid)
        {
            if (eid != null)
            {
                int? cid = _context.Enrolled.Find(eid).courseId;
                if (cid == null)
                {
                    return NotFound();
                }

                CourseViewModel thisView = new CourseViewModel(cid.GetValueOrDefault(), _context);
                thisView.setEnrolled( eid.GetValueOrDefault(),_context);

                return View(thisView);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> Drop(int? sid, int? eid)
        {
            if (sid != null && eid != null)
            {
                if (await functions.dropCourse(eid.GetValueOrDefault()))
                {
                    ViewBag.message = "<scipt>alert('Success Drop');</script>";
                }
                else
                {
                    ViewBag.message = "<scipt>alert('Failed Drop');</script>";
                }
                return RedirectToAction("Index", "Home", new { id = sid });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id","Name","Description","SectionId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Description", "SectionId")] Course course)
        {
            if (id != course.courseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // TODO : Handle if incorrect info is passed (IE. Changing Primary Key is invalid)
                    if (!CourseExists(course.courseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.courseId == id);
        }

        public async Task<IActionResult> RegisterPage(string searchString)
        {
            var courses = await _context.Courses.ToListAsync();
            courses = courses.GroupBy(course => course.courseName).Select(g => g.First()).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.FindAll(c => c.courseName.Contains(searchString));
            }

            return View(courses);
        }

        [HttpPost]
        public string RegisterPage(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]RegisterPage: " + searchString + "</h3>";
        }

        // GET: CourseInfo
        public async Task<IActionResult> CourseInfo(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.ToListAsync();
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses.FindAll(e => e.courseName == name));
        }

    }

}
