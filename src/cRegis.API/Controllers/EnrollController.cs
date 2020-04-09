using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cRegis.API.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly IEnrollService _enrollSerivce;

        public EnrollController(IEnrollService enrollService)
        {
            _enrollSerivce = enrollService;
        }

        [Route("[controller]/{eid}")]
        [HttpDelete]
        public ActionResult drop(int eid)
        {
            _enrollSerivce.drop(eid);
            return Ok();
        }

        [Route("[controller]/{eid}")]
        [HttpGet]
        public async Task<Enrolled> getEnrollAsync(int eid)
        {
            var enroll = await _enrollSerivce.getEnrollAsync(eid);

            return enroll;
        }

        [Route("[controller]")]
        [HttpPost]
        public async Task<ActionResult> updateEnroll(Enrolled newEnroll)
        {
            _enrollSerivce.updateEnroll(newEnroll);
            return Ok();
        }

        [Route("[controller]/all")]
        [HttpGet]
        public ActionResult<List<Enrolled>> getEnrollsForStudent()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var result = _enrollSerivce.getEnrollsForStudent(sid);

            return result;
        }

        [Route("[controller]/current")]
        [HttpGet]
        public ActionResult<List<Enrolled>> getCurrentEnrollsForStudent()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var result = _enrollSerivce.getCurrentEnrollsForStudent(sid);

            return result;
        }

        [Route("[controller]/completed")]
        [HttpGet]
        public ActionResult<List<Enrolled>> getCompletedEnrollsForStudent()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var result = _enrollSerivce.getCompletedEnrollsForStudent(sid);

            return result;
        }

    }
}