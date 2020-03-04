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
        private readonly IMapper mapper;
        private readonly Service services;
        private readonly UserManager<StudentUser> userManager;

        public RateCourseController(DataContext context, UserManager<StudentUser> userManager, IMapper mapper)
        {
            this.mapper = mapper;
            this.services = new Service(context);
            this.userManager= userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                Enrolled rateCourse = await services.findEnrollById(id);
                Course course = await services.findCourseById(rateCourse.courseId);
                //map all fields in Course to RateCourseViewModel to reduce effort of mapping them one-by-one
                RateCourseViewModel vm = mapper.Map<Enrolled, RateCourseViewModel>(rateCourse);
                mapper.Map<Course, RateCourseViewModel>(course);

                return View(vm);

            }catch(Exception e)
            {
                return View("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(RateCourseViewModel courseRate)
        {
            try
            {

                Enrolled newRating = await services.findEnrollById(courseRate.EnrollId);
                newRating.rating = courseRate.Rating;
                newRating.comment = courseRate.Comment;
                await services.updateEnroll(newRating);

                return RedirectToAction("Home/Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}