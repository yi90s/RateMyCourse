
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cRegis.Core.Identities;
using cRegis.Core.Interfaces;
using cRegis.Core.Entities;
using cRegis.Web.Interfaces;
using cRegis.Web.ViewModels;
using System.Collections.Generic;

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

            int index = await _studentSerivce.verifyDropForStudent(curUser.StudentId, eid);

            if (index == 0)
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

        [HttpGet]
        public List<Course> Index([FromQuery]string keywords)
        {
            List<Course> recCourses = _courseService.getCoursesByKeywords(keywords);

            return recCourses;
        }

    }

}
