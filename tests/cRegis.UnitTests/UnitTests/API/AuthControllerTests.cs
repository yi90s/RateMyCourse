using cRegis.API.Controllers;
using cRegis.API.Helpers;
using cRegis.Core.Identities;
using cRegis.UnitTests.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.UnitTests.UnitTests.API
{
    public class AuthControllerTests:APITestBase
    {
        private AuthController _controller;

        public AuthControllerTests():base()
        {
            _controller = new AuthController(_signInManager,_userManager, new Mock<IOptions<AppSettings>>().Object);
        }

        [Theory]
        [InlineData("jb:Password1!") ]
        public async Task getUserSuccessTestAsync(string authorization)
        {
            //arrange
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(authorization);
            string encoded = System.Convert.ToBase64String(plainTextBytes);
            encoded = " " + encoded;

            var result = await _controller.getUser(encoded);
            Assert.NotNull(result);
        }

    }
}
