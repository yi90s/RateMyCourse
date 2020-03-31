using cRegis.IntegrationTest.Helper;
using cRegis.IntegrationTest.Infrastructure;
using cRegis.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.IntegrationTest.UITests
{
    public class IndexPageTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IndexPageTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        //[Theory]
        //[InlineData(1)]
        [Fact]
        public async Task HttpGetPageTest()
        {
            // Arrange
            var provider = TestClaimsProvider.WithStudentClaims();
            var client = _factory.CreateClientWithTestAuth(provider);

            // Act
            var response = await client.GetAsync("/");


            //var UserName = content.QuerySelector("li[class='list-group-item']");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            //Assert.Equal("Welcome jb", UserName.InnerHtml);
        }
    }
}
