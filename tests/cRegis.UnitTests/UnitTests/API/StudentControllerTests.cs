using cRegis.API.Controllers;
using cRegis.Core.Entities;
using cRegis.UnitTests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.UnitTests.UnitTests.API
{
    public class StudentControllerTests:APITestBase
    {
        private readonly StudentController _controller;
        public StudentControllerTests():base()
        {
            _controller = new StudentController(_studentService);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task getStudentTest(int id)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.getStudent();

            Assert.NotNull(result);
            Assert.IsType<Student>(result);

        }

        [Theory]
        [InlineData(1,20, true)]
        [InlineData(7,6, false)]

        public async Task registerCourseForStudentTest(int id, int cid,bool success)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.registerCourseForStudent(cid);

            Assert.NotNull(result);
            if(success)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }


        [Theory]
        [InlineData(1,24)]
        [InlineData(3,48)]
        public void getRemainingCredithourForStudentTest(int id, int credit)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var result =  _controller.getRemainingCredithoursForStudent();

            Assert.NotNull(result);
            Assert.True(result == credit);



        }
    }
}
