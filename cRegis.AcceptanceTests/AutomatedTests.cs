using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace cRegis.AcceptanceTests
{
    public class AutomatedTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private string testingUrl = "http://ec2-15-222-137-75.ca-central-1.compute.amazonaws.com/";

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
        public void TestLogin()
        {
            _driver.Navigate().GoToUrl(testingUrl);
            _driver.FindElement(By.Id("UserName")).SendKeys("jb");
            _driver.FindElement(By.Id("Password")).SendKeys("Password1!");
            _driver.FindElement(By.Id("Password")).Submit();
            Assert.Contains("Welcome, John Braico", _driver.PageSource);
        }
    }
}
