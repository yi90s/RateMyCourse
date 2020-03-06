using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using cReg_WebApp.Models;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Services;
using cReg_WebApp.Controllers.Logic;
using cReg_WebApp.Models.context;
using System.Collections.Generic;
using System.Linq;
using cReg_WebApp.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
            this.services = new Service(context, userManager);
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



        public async Task<IActionResult> Register()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);

            List<Course> recomendCourses = await services.findRecommendCoursesForStudent(stu);
            return View(recomendCourses);
        }
        public async Task<IActionResult> Complete()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);

            CompleteCourseViewModel completeCourses = await services.createCompleteCourseViewModel(stu);
            return View(completeCourses);

        }

        public async Task<IActionResult> WishList()
        {
            var curUser = await userManager.GetUserAsync(this.User);
            Student stu = await services.findStudentById(curUser.StudentId);

            List<Course> wishListCourses = await services.findWishListCoursesForStudent(stu);
            return View(wishListCourses);
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
