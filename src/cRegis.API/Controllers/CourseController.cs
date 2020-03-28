using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using cRegis.Core.Interfaces;
using cRegis.Core.Entities;
using cRegis.Core.DTOs;

namespace cRegis.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Student")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseServices;

        public CourseController(ICourseService courseService)
        {
            _courseServices = courseService;
        }

        [Route("[controller]")]
        [HttpGet]
        public ActionResult<List<Course>> getCoursesByKeywords([FromQuery]string keywords)
        {
            List<Course> results = _courseServices.getCoursesByKeywords(keywords);

            return results;
        }

        [Route("[controller]")]
        [HttpGet]
        public ActionResult<List<Course>> getCoursesInYear([FromQuery]int year)
        {
            List<Course> result = _courseServices.getCoursesInYear(year);

            return result;
        }

        [Route("[controller]/recommend")]
        [HttpGet]
        public async Task<ActionResult<List<Course>>> getRecCoursesForStudentAsync()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var recCourses = await _courseServices.getRecCoursesForStudentAsync(sid);
           
            return recCourses;
        }

        [Route("[controller]/eligible")]
        [HttpGet]
        public async Task<ActionResult<List<Course>>> getEligibleCoursesForStudentAsync()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var eligibleCourses = await _courseServices.getEligibleCoursesForStudentAsync(sid);
      
            return eligibleCourses;
        }

        [Route("[controller]/taking")]
        [HttpGet]
        public async Task<ActionResult<List<Course>>> getTakingCourses()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var takingCourses = await _courseServices.getTakingCoursesForStudentAsync(sid);
           
            return takingCourses;
        }

        [Route("[controller]/completed")]
        [HttpGet]
        public ActionResult<List<Course>> getCompletedCourses()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var completedCourses = _courseServices.getCompletedCoursesForStudent(sid);
           
            return completedCourses;
        }

        [Route("[controller]/{cid}")]
        [HttpGet]
        public async Task<ActionResult<Course>> getCourseAsync(int cid)
        {
            Course result = await _courseServices.getCourseAsync(cid);

            return result;
        }


        [Route("[controller]/{cid}/comments")]
        [HttpGet]
        public ActionResult<List<Comment>> getCommentsForCourse(int cid)
        {
            List<Comment> result =  _courseServices.getCommentsForCourse(cid);

            return result;
        }


    }
}