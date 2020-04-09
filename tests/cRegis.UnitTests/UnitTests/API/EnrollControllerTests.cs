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
    public class EnrollControllerTests:APITestBase
    {
        private EnrollController _controller;
        public EnrollControllerTests():base()
        {
            _controller = new EnrollController(_enrollService);
        }

        [Theory]
        [InlineData(23)]
        public void dropTest(int eid)
        {
            var result = _controller.drop(eid);
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(13)]
        public async Task getEnrollTest(int eid)
        {
            var result = await _controller.getEnrollAsync(eid);
            Assert.NotNull(result);
            Assert.IsType<Enrolled>(result);
        }

        [Theory]
        [InlineData(33, 4, 6,true,80, null, null)]
        [InlineData(27, 3, 4, true, 70, 58, "good")]
        [InlineData(42, 6, 4, true, 88, 68, "bad")]
        public async Task updateEnrollTest(int eid,int sid, int cid, bool c, int g, int r, string co)
        {
            Enrolled newEnroll = new Enrolled { enrollId = eid, studentId = sid, courseId = cid, completed = c, grade = g, rating = r, comment = co };
            var result = await _controller.updateEnroll(newEnroll);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }


        [Theory]
        [InlineData(2,11)]
        [InlineData(3, 5)]
        [InlineData(4, 5)]
        public void getEnrollsForStudentTest(int id, int num)
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

            var result =  _controller.getEnrollsForStudent();

            Assert.NotNull(result);
            Assert.True(result.Value.Count == num);

        }


        [Theory]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        public void getCurrentEnrollsForStudentTest(int id, int num)
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

            var result = _controller.getCurrentEnrollsForStudent();

            Assert.NotNull(result);
            Assert.True(result.Value.Count == num);
        }


        [Theory]
        [InlineData(2, 10)]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        public void getCompleteEnrollsForStudentTest(int id, int num)
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

            var result = _controller.getCompletedEnrollsForStudent();

            Assert.NotNull(result);
            Assert.True(result.Value.Count == num);
        }
    }
}
