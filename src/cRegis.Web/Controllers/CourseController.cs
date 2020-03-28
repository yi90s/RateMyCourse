
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cRegis.Core.Identities;
using cRegis.Core.Interfaces;
using cRegis.Core.Entities;
using cRegis.Web.Interfaces;
using cRegis.Web.ViewModels;

namespace cRegis.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class CourseController : Controller
    {

        private readonly UserManager<StudentUser> _userManager;
        private readonly SignInManager<StudentUser> _signInManager;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentSerivce;
        private readonly IEnrollService _enrollSerivce;
        private readonly IViewModelService _viewModelSerivce;

        public CourseController(UserManager<StudentUser> userManager,
                              SignInManager<StudentUser> signInManager,
                              ICourseService courserSerivce,
                              IStudentService studentSerivce,
                              IEnrollService enrollSerivce,
                              IViewModelService viewModelSerivce)
        {
            _viewModelSerivce = viewModelSerivce;
            _enrollSerivce = enrollSerivce;
            _studentSerivce = studentSerivce;
            _courseService = courserSerivce;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Register(int cid)
        {
            var curUser = await _userManager.GetUserAsync(this.User);
            int sid = curUser.StudentId;

            if (await _studentSerivce.verifyRegistrationForStudent(sid, cid) == 0)
            {
                await _studentSerivce.registerCourseForStudent(sid, cid);
                TempData["alertMessage"] = "Success Registration";
            }
            else
            {
                TempData["alertMessage"] = "Failed Registration";
            }
            return RedirectToAction("Register", "Home");
        }

        public async Task<IActionResult> Drop(int eid)
        {
            var curUser = await _userManager.GetUserAsync(this.User);

            if (_studentSerivce.verifyDropForStudent(curUser.StudentId, eid).Equals(0))
            {
                TempData["alertMessage"] = "Success Drop";
                _enrollSerivce.drop(eid);
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
            Enrolled rate = await _enrollSerivce.getEnrollAsync(eid);
            Course course = await _courseService.getCourseAsync(rate.courseId);
            RateCourseViewModel vm = _viewModelSerivce.buildRateCourseViewModel(rate, course);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Rate(RateCourseViewModel courseRate)
        {
            Enrolled newRating = await _enrollSerivce.getEnrollAsync(courseRate.EnrollId);
            newRating.rating = courseRate.Rating;
            newRating.comment = courseRate.Comment;
            _enrollSerivce.updateEnroll(newRating);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Detail(int cid)
        {
            CourseDetailViewModel vm = _viewModelSerivce.buildCourseDetailViewModel(cid);

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
