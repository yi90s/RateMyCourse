using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cReg_WebApp.Models;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Services;
using cReg_WebApp.Models.context;
using System.Collections.Generic;
using cReg_WebApp.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using cReg_WebApp.Models.ViewModels.HomeViewModels;

namespace cReg_WebApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class 
        HomeController : Controller
    {

        private readonly Service services;
        private readonly UserManager<StudentUser> userManager;
        private readonly SignInManager<StudentUser> signInManager;

        public HomeController(DataContext context, 
                              UserManager<StudentUser> userManager, 
                              SignInManager<StudentUser> signInManager)
        {
            this.services = new Service(context);
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            //get instance of current StudentUser oboject
            var curUser = await userManager.GetUserAsync(this.User);
            Student student = await services.findStudentById(curUser.StudentId);
            ProfileViewModel thisView = await services.createProfileViewModel(student);
            return View(thisView);
         }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);

            FindCourseViewModel thisView = await services.createFindCourseViewModel(stu);
            return View(thisView);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);

            HistoryViewModel completeCourses = await services.createHistoryViewModel(stu);
            return View(completeCourses);

        }

        [HttpGet]
        public async Task<IActionResult> WishList()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);
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
