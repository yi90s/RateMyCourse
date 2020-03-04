using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models;
using cReg_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<StudentUser> _signInManager;

        public AuthController(SignInManager<StudentUser> _signInManager)
        {
            _signInManager = _signInManager;
        }

        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);


        }
    }
}