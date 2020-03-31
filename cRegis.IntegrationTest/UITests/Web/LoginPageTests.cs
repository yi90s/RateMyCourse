using cRegis.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.IntegrationTest.UITests
{
    public class LoginPageTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LoginPageTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task HttpGetPageTest()
        {
            var client = _factory.CreateClient();

        }
    }
}
