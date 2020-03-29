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
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [Route("[controller]/{fid}")]
        public ActionResult<Faculty> getFaculty(int fid)
        {
            return _facultyService.getFaculty(fid);
        }
    }
}