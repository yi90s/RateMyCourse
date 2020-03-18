using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cRegis.Core.Interfaces;
using cRegis.Core.Identities;
using cRegis.Core.Entities;

namespace cRegis.Web.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]

    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseServices;

        public CoursesController(UserManager<StudentUser> userManager,
            ICourseService courseService)
        {
            _courseServices = courseService;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Index(string keywords)
        {

            List<Course> results = _courseServices.getCoursesByKeywords(keywords);

            if(results == null || results.Count == 0)
            {
                return NotFound();
            }

            return results;
        }


    }
}