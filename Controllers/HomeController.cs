using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using cReg_WebApp.Models;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.SQL;
using cReg_WebApp.Models.context;
using System.Linq;

namespace cReg_WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;

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
            return View();
        }

        public string Index()
        {
            var firstStu = _context.Students.ToList();
            return firstStu[0].name;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
