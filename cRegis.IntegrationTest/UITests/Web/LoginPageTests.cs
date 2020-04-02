using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.UITests
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
            //Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(response);


            var form = content.QuerySelector("form[id='form1']");
            var inputs = form.Children;

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            Assert.True(inputs.Length == 7);
            Assert.Equal("LABEL", inputs[0].TagName);
            Assert.Equal("INPUT", inputs[1].TagName);
            Assert.Equal("LABEL", inputs[2].TagName);
            Assert.Equal("INPUT", inputs[3].TagName);
            Assert.Equal("DIV", inputs[4].TagName);
            Assert.Equal("INPUT", inputs[5].TagName);
        }
    }
}
