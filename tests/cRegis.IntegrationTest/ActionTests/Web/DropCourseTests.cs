using cRegis.Core.Entities;
using cRegis.Tests.IntegrationTest.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.ActionTests.Web
{
    public class DropCourseTests:WebActionTestBase
    {
        public DropCourseTests():base() { }

        [Theory]
        [InlineData("jb",10, false)]
        [InlineData("pg",28, true)]
        [InlineData("jb",45, false)]
        public async Task Test( string accountId,int enrollId, bool success)
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
            Enrolled thisEnroll = await _enrollService.getEnrollAsync(enrollId);
            if(thisEnroll!=null)
            {
                var action = (RedirectToActionResult)await _courseController.Drop(enrollId);

                //assert
                if(success)
                {
                    Assert.Equal("Success Drop", _courseController.TempData["alertMessage"]);
                    Assert.Null(_context.Enrolled.Find(enrollId));
                }
                else
                {
                    Assert.Equal("Failed Drop", _courseController.TempData["alertMessage"]);
                }
                Assert.NotNull(action);
                Assert.Equal("Index", action.ActionName);
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
