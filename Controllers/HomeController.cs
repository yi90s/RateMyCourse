﻿using System;
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
    public class HomeController : Controller
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
            try
            {
                //get instance of current StudentUser oboject
                var curUser = await userManager.GetUserAsync(this.User);
                Student student = await services.findStudentById(curUser.StudentId);
                List<Enrolled> takingCourses = await services.findAllCurrentEnrollsForStudent(student);
                ProfileViewModel thisView = await services.createProfileViewModel(student.studentId);
                if (thisView.thisStudent != null)
                {
                    return View(thisView);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }


            }catch(Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
            

        public async Task<IActionResult> Register()
        {
                var curUser = await userManager.GetUserAsync(this.User);
                Student stu = await services.findStudentById(curUser.StudentId);
            if (stu!=null)
            {
                ViewData["studentId"] = stu.studentId;
                @ViewData["Name"] = stu.name;
                List<int> takingCourseId =await _context.Enrolled.Where(e => (e.studentId == stu.studentId && !e.completed)).Select(e => e.courseId).ToListAsync();
                List<Course> courseList =await _context.Courses.Where(c => !takingCourseId.Contains(c.courseId)).ToListAsync();
                return View(courseList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
            public async Task<IActionResult> Complete(int id)
            {
                Student stu = await _context.Students.FindAsync(id);
                if (stu != null)
                {
                    ViewData["studentId"] = stu.studentId;
                    @ViewData["Name"] = stu.name;


                    List<int> takingCourseId = await _context.Enrolled.Where(c => c.studentId == stu.studentId && c.completed).Select(c => c.courseId).ToListAsync();
                    List<Course> registeredCourses = await _context.Courses.Where(c => takingCourseId.Contains(c.courseId)).ToListAsync();
                    return View(registeredCourses);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            //TODO
            public IActionResult WishList(int id)
            {
                Student stu = _context.Students.Find(id);
                if (stu != null)
                {
                    ViewData["studentId"] = stu.studentId;
                    @ViewData["Name"] = stu.name;
                    return View(stu);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }





        //TODO: Allow users to sign out
        [HttpGet]
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
