using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cReg_WebApp.Models;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.SQL;
using cReg_WebApp.Controllers.Logic;
using cReg_WebApp.Models.context;

namespace cReg_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string StudentID, string Password)
        {
            int id = int.Parse(StudentID);
            Student stu = _context.Students.Find(id);
            if (stu != null)
            {
                if (stu.password.Equals(Password))
                {
                    string url = string.Format("/home/index?studentId={0}", stu.studentId);
                    return Redirect(url);
                }
                else
                {
                    ViewBag.Message = "student Id or password is invalid";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "student Id or password is invalid";
                return View();
            }
        }


        public IActionResult Index()
        {
            if(!String.IsNullOrEmpty(Request.Query["studentId"]))
            {
                Student stu = _context.Students.Find(int.Parse(Request.Query["studentId"]));
                return View(stu);
            }
            else
            {
                return RedirectToAction("Home", "Login");
            }
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> RegisterPage()
        //{
        //    var courses = await _context.Course.ToListAsync();
        //    return View(courses.GroupBy(course => course.Name).Select(g => g.First()).ToList());
        //}


        //// GET: CourseInfo
        //public async Task<IActionResult> CourseInfo(string? name)
        //{
        //    if (name == null)
        //    {
        //        return NotFound();
        //    }

        //    var courses = await _context.Course.ToListAsync();
        //    if (courses == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(courses.FindAll(e => e.Name == name));
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
