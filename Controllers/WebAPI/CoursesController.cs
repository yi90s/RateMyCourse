using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Student")]
    public class CoursesController : ControllerBase
    {
        private readonly Service _services;

        public CoursesController(DataContext context, UserManager<StudentUser> userManager)
        {
            _services = new Service(context);
            
        }


    }
}