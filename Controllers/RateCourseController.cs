using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels;
using cReg_WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class RateCourseController : Controller
    {

        private readonly Service services;
        private readonly UserManager<StudentUser> userManager;

        public RateCourseController(DataContext context, UserManager<StudentUser> userManager)
        {
            this.services = new Service(context, userManager);
            this.userManager= userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
           
            Enrolled rate = await services.findEnrollById(id);
            Course course = await services.findCourseById(rate.courseId);
            RateCourseViewModel vm = new RateCourseViewModel(rate, course);

            return View(vm);

 
        }

        [HttpPost]
        public async Task<IActionResult> Index(RateCourseViewModel courseRate)
        {
           
            Enrolled newRating = await services.findEnrollById(courseRate.EnrollId);
            newRating.rating = courseRate.Rating;
            newRating.comment = courseRate.Comment;
            services.updateEnroll(newRating);

            return RedirectToAction("Home/Index");


        }
    }
}