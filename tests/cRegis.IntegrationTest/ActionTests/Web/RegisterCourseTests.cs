using cRegis.Core.Entities;
using cRegis.Tests.IntegrationTest.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.ActionTests.Web
{
    public class RegisterCourseTests : WebActionTestBase
    {
        public RegisterCourseTests():base(){}


        [Theory]
        [InlineData("jb",1, 10, true)]
        [InlineData("fb",7, 6, false)]
        [InlineData("jb",1, 45, false)]
        [InlineData("mz",1,15, false)]
        public async Task Test(string accountId,int sid, int courseId, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
           {
                    new Claim(ClaimTypes.Name, accountId)
           }));
            _courseController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _courseController.TempData = tempData;

            //act
            Course thisCourse = _courseService.getCourse(courseId);
            if (thisCourse != null)
            {
                var action = (RedirectToActionResult)await _courseController.Register(courseId);
                List<Enrolled> enrolls = _context.Enrolled.Where(e => e.studentId == sid && e.courseId == courseId && !e.completed).ToList();

                //assert
                if (success)
                {
                    Assert.Equal("Success Registration", _courseController.TempData["alertMessage"]);
                    Assert.NotEmpty(enrolls);
                }
                else
                {
                    Assert.Equal("Failed Registration", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Register", action.ActionName);
                Assert.Equal("Home", action.ControllerName);
            }
            else
            {
                //assert
                Assert.False(success);
            }
        }
    }
}
