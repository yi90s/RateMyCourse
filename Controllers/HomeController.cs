using cReg_WebApp.Controllers.Logic;
using cReg_WebApp.Data;
using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;



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
            Recaptcha newRecap = new Recaptcha(Request.Form["g-recaptcha-response"]);
            if (newRecap.IsReCaptchValid())
            {
                int id = int.Parse(StudentID);
                DbInitializer.Initialize(_context);
                Student stu = _context.Students.Find(id);
                if (stu != null)
                {
                    if (stu.Password.Equals(Password))
                    {
                        string url = string.Format("/home/index?studentId={0}", stu.StudentId);
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
            else
            {
                ViewBag.Message = "Recapture is invalid";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            DbInitializer.Initialize(_context);
            if (!string.IsNullOrEmpty(Request.Query["studentId"]))
            {
                List<Course> registeredCourses = new List<Course>();
                Student stu = _context.Students.Find(int.Parse(Request.Query["studentId"]));
                List<Enrolled> courses = _context.Enrolled.Where(c => c.StudentId == stu.StudentId).ToList();
                foreach (Enrolled var in courses)
                {
                    int courseId = var.CourseId;
                    registeredCourses.Add(_context.Courses.Find(courseId));
                }
                ViewBag.stu = stu;
                ViewBag.registeredCourses = registeredCourses;
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Login");
            }
        }
        

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
