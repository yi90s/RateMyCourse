using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using cRegis.Core.Identities;
using cRegis.Core.Interfaces;
using cRegis.Web.Interfaces;
using cRegis.Core.Entities;
using cRegis.Web.ViewModels;

namespace cRegis.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class 
        HomeController : Controller
    {

        private readonly UserManager<StudentUser> _userManager;
        private readonly SignInManager<StudentUser> _signInManager;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentSerivce;
        private readonly IEnrollService _enrollSerivce;
        private readonly IViewModelService _viewModelSerivce;

        public HomeController(UserManager<StudentUser> userManager,
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //get instance of current StudentUser oboject
            var curUser = await _userManager.GetUserAsync(this.User);
            Student student = await _studentSerivce.getStudentAsync(curUser.StudentId);
            ProfileViewModel thisView = _viewModelSerivce.buildProfileViewModel(student);

            return View(thisView);
         }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var curUser = await _userManager.GetUserAsync(this.User);
            Student stu = await _studentSerivce.getStudentAsync(curUser.StudentId);
            FindCourseViewModel thisView =  await _viewModelSerivce.buildFindCourseViewModelAsync(stu);

            return View(thisView);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var curUser = await _userManager.GetUserAsync(this.User);
            Student stu = await _studentSerivce.getStudentAsync(curUser.StudentId);

            HistoryViewModel completeCourses =  _viewModelSerivce.buildHistoryViewModel(stu);
            return View(completeCourses);

        }

        [HttpGet]
        public async Task<IActionResult> WishList()
        {
            var curUser = await _userManager.GetUserAsync(this.User);
            Student stu = await _studentSerivce.getStudentAsync(curUser.StudentId);
            return View(stu);

            //WishListViewModel thisView = await services.createWishListViewModel(stu);
            //return View(thisView);
        }




        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
