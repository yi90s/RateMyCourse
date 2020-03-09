using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cReg_WebApp.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]

    public class CoursesController : ControllerBase
    {
        private readonly Service _services;

        public CoursesController(DataContext context, UserManager<StudentUser> userManager)
        {
            _services = new Service(context);
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Index(string keywords)
        {

            List<Course> results = await _services.findCoursesByKeyWords(keywords);

            if(results == null || results.Count == 0)
            {
                return NotFound();
            }

            return results;
        }


    }
}