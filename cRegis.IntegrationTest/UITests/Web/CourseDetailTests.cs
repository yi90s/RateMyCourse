using cRegis.Tests;
using cRegis.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace cRegis.Tests.IntegrationTest.UITests
{
    public class CourseDetailTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public CourseDetailTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task HttpGetPageTest()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/course/Detail?cid=1");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
