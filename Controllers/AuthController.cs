using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models;
using cReg_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<StudentUser>  signInManager;
        public AuthController(SignInManager<StudentUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //if user is already logged in
            bool loggedIn = this.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                return RedirectToAction("Index");
            }

            return await Task.Run(() => View());
        }

        
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Incorrect user name or password";
                return RedirectToAction("Index");
            }

            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            //for unknown reason login failed
            return RedirectToAction("Index");

        }
    }
}