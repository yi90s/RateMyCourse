using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace cRegis.AcceptanceTests
{
    public class AutomatedTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public AutomatedTests()
        {
            _driver = new FirefoxDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        //Tests

        [Fact]
        public void Create_WhenExecuted_ReturnsCreateView()
        {
            _driver.Navigate().GoToUrl("https://localhost:44353/Home");
        }
    }
}
