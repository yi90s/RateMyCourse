using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.ViewModels;
using cReg_WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<StudentUser>  signInManager;
        private readonly UserManager<StudentUser>  userManager;
        private readonly Service services;

        public AuthController(SignInManager<StudentUser> signInManager, UserManager<StudentUser> userManager, DataContext context)
        {
            this.services = new Service(context);
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //if user is already logged in
            bool loggedIn = this.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                return RedirectToAction("Index", "Home");
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

        
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return await AccessDenied();
        }
    }
}