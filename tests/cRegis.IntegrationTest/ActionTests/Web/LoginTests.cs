using cRegis.Core.Identities;
using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.ActionTests.Web
{
    public class LoginTests : WebActionTestBase
    {
        public LoginTests() : base() { }

        [Fact]
        public async Task Autherized_Test()
        {
            //arrange
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.Identity.IsAuthenticated).Returns(true);
            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user.Object }
            };

            //Act
            var result = (RedirectToActionResult)await _authController.Index();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);

        }

        [Theory]
        [InlineData("jb", "Password1!", true)]
        public async Task UnAutherized_Test(string id, string password, bool success)
        {
            //arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim(ClaimTypes.Name, id)
             }));
            _authController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            //act
            var result = (RedirectToActionResult) await _authController.Index(new LoginViewModel { UserName = id, Password = password });

            if (success)
            {
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
                Assert.Equal("Home", result.ControllerName);
            }
            else
            {
                Assert.NotNull(result);
                Assert.Null(result.ActionName);
                Assert.Equal("Home", result.ControllerName);
            }
        }
    }
}
