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
    public class WishlistControllerTests : APITestBase
    {
        private readonly WishlistController _controller;
        public WishlistControllerTests() : base()
        {
            _controller = new WishlistController(_wishlistService);
        }

        [Theory]
        [InlineData(1, 6, true)] //Happy Path
        [InlineData(1, 1, false)] //Entry Already Exists
        [InlineData(-1, 6, false)] //Non-Existent Student
        [InlineData(1, -1, false)] //Non-Existent Course
        public async Task addCoursetoStudentWishlist(int sid, int cid, bool success)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",sid.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.addCoursetoStudentWishlist(cid);

            Assert.NotNull(result);
            if (success)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(1, 4, true)] //Happy Path
        [InlineData(1, 6, false)] //Non-Existent Entry
        public void removeCourseFromStudentWishlistTest(int sid, int cid, bool success)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",sid.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = _controller.removeCourseFromStudentWishlist(cid);

            Assert.NotNull(result);
            if (success)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData(1, 2)] //Happy Path
        [InlineData(1, 1)] //No Priority Change
        public async Task updatePriorityUpTest(int sid, int cid)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",sid.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.updatePriorityUp(cid);

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(1, 2)] //Happy Path
        [InlineData(1, 5)] //No Priority Change
        public async Task updatePriorityDownTest(int sid, int cid)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",sid.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.updatePriorityDown(cid);

            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(1, 5)] //Happy Path
        [InlineData(2, 4)] //Happy Path
        [InlineData(-1, 0)] //Non-Existent Student
        public void getStudentWishlistTest(int sid, int numEntries)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",sid.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = _controller.getStudentWishlist();

            Assert.NotNull(result);
            Assert.True(result.Value.Count == numEntries);
        }

    }
}
