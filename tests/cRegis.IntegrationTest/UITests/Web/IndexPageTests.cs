using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.UITests
{
    public class IndexPageTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IndexPageTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task HttpGetPageTest()
        {
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Home");
            var content = await HtmlHelpers.GetDocumentAsync(response);


            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            //Assert.Equal("Welcome jb", UserName.InnerHtml);
        }
    }
}
