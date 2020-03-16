
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.ViewModels;
using cReg_WebApp.Models.ViewModels.CourseViewModels;
using System;
using Microsoft.AspNetCore.Authorization;
using cReg_WebApp.Services;
using Microsoft.AspNetCore.Identity;
using cReg_WebApp.Models;

namespace cReg_WebApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class CourseController : Controller
    {

        private readonly Service services;
        private readonly UserManager<StudentUser> userManager;
        private readonly SignInManager<StudentUser> signInManager;

        public CourseController(DataContext context,
                              UserManager<StudentUser> userManager,
                              SignInManager<StudentUser> signInManager)
        {
            this.services = new Service(context);
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    

        [HttpGet]
        public async Task<IActionResult> RegisterDetails(int cid)
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);
            if(services.verifyRegisterDetailForStudents(stu,cid))
            {
                CourseViewModel thisModel = services.createCourseViewModel(cid);

                return View(thisModel);
            }
            else
            {
                TempData["alertMessage"] = "Url Error: Redirect to Find Course page";
                return RedirectToAction("Register", "Home");
            }
        }

        public async Task<IActionResult> Register(int cid)
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);
            if (await services.verifyRegistrationForStudent(stu,cid))
            {
                await services.registerCourseForStudent(stu, cid);
                TempData["alertMessage"] = "Success Registration";
            }
            else
            {
                TempData["alertMessage"]  = "Failed Registration";
            }
            return RedirectToAction("Register","Home");
        }

        [HttpGet]
        public async Task<IActionResult> DropDetails(int eid)
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);
            if (services.verifyDropDetailForStudents(stu, eid))
            {
                Enrolled thisEnroll = await services.findEnrollById(eid);

                CourseViewModel thisView = services.createCourseViewModel(thisEnroll.courseId, enroll:thisEnroll);
                thisView.enrollId = eid;
                return View(thisView);
            }
            else
            {
                TempData["alertMessage"] = "Url Error: Redirect to Profile page";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Drop(int eid)
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);
            if (await services.verifyDropForStudent(stu, eid))
            {
                TempData["alertMessage"] = "Success Drop";
                await services.dropCourseForStudent(eid);
            }
            else
            {
                TempData["alertMessage"] = "Failed Drop";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Rate(int eid)
        {

            Enrolled rate = await services.findEnrollById(eid);
            Course course = await services.findCourseById(rate.courseId);
            RateCourseViewModel vm = new RateCourseViewModel(rate, course);

            return View(vm);


        }

        [HttpPost]
        public async Task<IActionResult> Rate(RateCourseViewModel courseRate)
        {

            Enrolled newRating = await services.findEnrollById(courseRate.EnrollId);
            newRating.rating = courseRate.Rating;
            newRating.comment = courseRate.Comment;
            services.updateEnroll(newRating);

            return RedirectToAction("Home/Index");


        }

        [HttpGet]
        public async Task<IActionResult> Detail(int cid)
        {
            CourseDetailViewModel vm = await services.createCourseDetailViewModel(cid);

            return View(vm);
        }

        //// GET: Course/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Course/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id", "Name", "Description", "SectionId")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(course);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(course);
        //}

        //// GET: Course/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses.FindAsync(id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(course);
        //}

        //// POST: Course/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id", "Name", "Description", "SectionId")] Course course)
        //{
        //    if (id != course.courseId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(course);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            // TODO : Handle if incorrect info is passed (IE. Changing Primary Key is invalid)
        //            if (!CourseExists(course.courseId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(course);
        //}

        //// GET: Course/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .FirstOrDefaultAsync(m => m.courseId == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        //// POST: Course/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var course = await _context.Courses.FindAsync(id);
        //    _context.Courses.Remove(course);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CourseExists(int id)
        //{
        //    return _context.Courses.Any(e => e.courseId == id);
        //}

        //public async Task<IActionResult> RegisterPage(string searchString)
        //{
        //    var courses = await _context.Courses.ToListAsync();
        //    courses = courses.GroupBy(course => course.courseName).Select(g => g.First()).ToList();

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        courses = courses.FindAll(c => c.courseName.Contains(searchString));
        //    }

        //    return View(courses);
        //}

        //[HttpPost]
        //public string RegisterPage(FormCollection fc, string searchString)
        //{
        //    return "<h3> From [HttpPost]RegisterPage: " + searchString + "</h3>";
        //}

        //// GET: CourseInfo
        //public async Task<IActionResult> CourseInfo(string name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }

        //    var courses = await _context.Courses.ToListAsync();
        //    if (courses == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(courses.FindAll(e => e.courseName == name));
        //}

    }

}
