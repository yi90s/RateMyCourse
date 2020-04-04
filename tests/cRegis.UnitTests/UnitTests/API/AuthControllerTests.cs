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
            var result = await _controller.getUser(authorization);
            Assert.NotNull(result);
            Assert.True((result.GetType()).Equals(typeof(StudentUser)));
        }

    }
}
