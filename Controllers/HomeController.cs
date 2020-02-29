using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using cReg_WebApp.Models;
using cReg_WebApp.Models.entities;
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

namespace cReg_WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;
        private UserManager<StudentUser> userManager;
        private SignInManager<StudentUser> signInManager;

        public HomeController(DataContext context, 
                              UserManager<StudentUser> userManager, 
                              SignInManager<StudentUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task <IActionResult> Login()    
        {

            //if (result.Succeeded)
            //{
            return await Task.Run(() => View());
            //}

            //var errors = "";
            //foreach( var error in result.Errors)
            //{
            //    errors += error.Description;
            //}
            //return Content(errors);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string StudentID, string Password)
        {
            int id = int.Parse(StudentID);
            Student stu = await _context.Students.FindAsync(id);
            Boolean pass = false;


            //if (stu != null)
            //{
            //    if (stu.password.Equals(Password))
            //    {
            //        HttpContext.Response.Cookies.Append("studentId", StudentID);
            //        pass = true;
            //    }
            //}

            //if (pass)
            //{
            //    return RedirectToAction("Index", "Home", new { id = stu.studentId });
            //}
            //else
            //{
            //    ViewBag.Message = "student Id or password is invalid";
            return View("Login");
            //}
        }


        public async Task<IActionResult> Index(int id)
        {
            Student stu = await _context.Students.FindAsync(id);
            if (stu != null)
            {
                ViewData["studentId"] = stu.studentId;
                ViewData["Name"] = stu.name;
                Faculty major = _context.Faculties.Find(stu.majorId);
                ViewData["majorName"] = major.facultyName;
                List<int> takingCourseId = await _context.Enrolled.Where(c => c.studentId == stu.studentId && !c.completed).Select(c => c.courseId).ToListAsync();
                List<Course> registeredCourses = await _context.Courses.Where(c => takingCourseId.Contains(c.courseId)).ToListAsync();

                return View(registeredCourses);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public async Task<IActionResult> Register(int id)
        {
            Student stu = await _context.Students.FindAsync(id);
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


        [HttpGet]
        public async Task<IActionResult> RateCourse(int id)
        {
            try
            {
                Enrolled enroll = await _context.Enrolled.FindAsync(id);
                Course courseDetail = await _context.Courses.FindAsync(enroll.courseId);
                var rateCourseVM = new RateCourseViewModel (enroll, courseDetail);

                return View(rateCourseVM);
            }catch (Exception ex)
            {
                return RedirectToAction("Error: "+ ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourseRate(RateCourseViewModel courseRate)
        {
            try
            {
                Enrolled updated =await _context.Enrolled.FindAsync(courseRate.EnrollId);
                updated.rating = courseRate.Rating;
                updated.comment = courseRate.Comment;
                _context.Enrolled.Update(updated);
                _context.SaveChanges();
                return RedirectToAction("Home/Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error: "+ex);
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
