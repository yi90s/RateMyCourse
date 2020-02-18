using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
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
        public async Task<IActionResult> Create([Bind("CourseId", "CourseName", "CourseDescription")] Course course)
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
        public async Task<IActionResult> Edit(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(courseId);
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
        public async Task<IActionResult> Edit(int courseId, [Bind("CourseId", "CourseName", "CourseDescription")] Course course)
        {
            if (courseId != course.CourseId)
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
                    if (!CourseExists(course.CourseId))
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
        public async Task<IActionResult> Delete(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == courseId);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("CourseId", "CourseName", "CourseDescription")] Course course)
        {
            var tempCourse = _context.Courses.Find(course.CourseId);
            _context.Courses.Remove(tempCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int courseId)
        {
            return _context.Courses.Any(e => e.CourseId == courseId);
        }

        public async Task<IActionResult> RegisterPage(string searchString)
        {
            var courses = await _context.Courses.ToListAsync();
            courses = courses.GroupBy(course => course.CourseName).Select(g => g.First()).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.FindAll(c => c.CourseName.Contains(searchString));
            }

            return View(courses);
        }

        [HttpPost]
        public string RegisterPage(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]RegisterPage: " + searchString + "</h3>";
        }

        // GET: CourseInfo
        public async Task<IActionResult> CourseInfo(string? name)
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
            return View(courses.FindAll(e => e.CourseName == name));
        }
    }

}
