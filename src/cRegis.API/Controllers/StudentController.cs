using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace cRegis.API.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Route("[controller]")]
        [HttpGet]
        public async Task<Student> getStudent()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            return await _studentService.getStudentAsync(sid);
        }
        
        [Route("[controller]/register/{cid}")]
        [HttpPost]
        public async Task<ActionResult> registerCourseForStudent(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            int valid = await _studentService.verifyRegistrationForStudent(sid, cid);

            if (valid != 0)
            {
                return BadRequest("Student is not able to register the course");
            }
            
            _studentService.registerCourseForStudent(sid, cid);
            return Ok("Successful Registration");
        }

        [Route("[controller]/credhrs")]
        [HttpGet]
        public int getRemainingCredithoursForStudent()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);

            return _studentService.getRemainingCredithoursForStudent(sid);
        }


    }
}