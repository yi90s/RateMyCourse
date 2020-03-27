using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using cRegis.Core.Interfaces;
using cRegis.Core.Identities;
using cRegis.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace cRegis.API.Controllers
{
    [ApiController]
    [Authorize(Roles="Student")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseServices;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public CourseController(ICourseService courseService)
        {
            _courseServices = courseService;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [Route("[controller]")]
        [HttpGet]
        public ActionResult<List<Course>> getCoursesByKeywords([FromQuery]string keywords)
        {

            List<Course> results = _courseServices.getCoursesByKeywords(keywords);

            return results;
        }

        [Route("[controller]")]
        public ActionResult<List<Course>> getCoursesInYear(int year)
        {
            List<Course> result = _courseServices.getCoursesInYear(year);

            return result;
        }

        [Route("[controller]/recommend")]
        public async Task<ActionResult<List<Course>>> getRecCourses()
        {
            List<Course> recCourses = new List<Course>();
            
            foreach(var claim in this.User.Claims)
            {
                if(claim.Type == "sid")
                {
                    recCourses = await _courseServices.getRecCoursesForStudentAsync(Int32.Parse(claim.Value));
                }
            }
            
            return recCourses;
        }

        [Route("[controller]/eligible")]
        public async Task<ActionResult<List<Course>>> getEligibleCourses()
        {
            List<Course> eligibleCourses = new List<Course>();
            foreach (var claim in this.User.Claims)
            {
                if (claim.Type == "sid")
                {
                    eligibleCourses = await _courseServices.getEligibleCoursesForStudentAsync(Int32.Parse(claim.Value));
                }
            }

            return eligibleCourses;
        }

        [Route("[controller]/taking")]
        public async Task<ActionResult<List<Course>>> getTakingCourses()
        {
            List<Course> takingCourses = new List<Course>();
            foreach (var claim in this.User.Claims)
            {
                if (claim.Type == "sid")
                {
                    takingCourses = await _courseServices.getTakingCoursesForStudentAsync(Int32.Parse(claim.Value));
                }
            }

            return takingCourses;
        }

        [Route("[controller]/completed")]
        public ActionResult<List<Course>> getCompletedCourses()
        {
            List<Course> completedCourses = new List<Course>();
            foreach (var claim in this.User.Claims)
            {
                if (claim.Type == "sid")
                {
                    completedCourses = _courseServices.getCompletedCoursesForStudent(Int32.Parse(claim.Value));
                }
            }

            return completedCourses;
        }

        [Route("[controller]/{id}")]
        [HttpGet]
        public async Task<ActionResult<Course>> getCourse(int id)
        {
            Course result = await _courseServices.getCourseAsync(id);

            return result;
        }







    }
}