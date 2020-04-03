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

        //Helpers

        private void signin()
        {
            _driver.Navigate().GoToUrl(testingUrl);
            _driver.FindElement(By.Id("UserName")).SendKeys("jb");
            _driver.FindElement(By.Id("Password")).SendKeys("Password1!");
            _driver.FindElement(By.Id("Password")).Submit();
        }

        private void signout()
        {
            _driver.FindElement(By.XPath("/html/body/header/nav/div/div[2]/input")).Click();
        }

        //TESTS

        [Fact]
        public void Signin_Signout_Test()
        {
            signin();
            Assert.Contains("Welcome, John Braico", _driver.PageSource);
            signout();
            Assert.DoesNotContain("Welcome, John Braico", _driver.PageSource);
        }

        [Fact]
        public void FindCourse_RegisterCourse_Verify_DropCourse_Verify_Test()
        {
            signin();

            //Verify the the student is currently not enrolled the "Calculus 2"
            Assert.DoesNotContain("Calculus 2", _driver.PageSource);

            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();
            Assert.Contains("Calculus 2", _driver.PageSource);

            //Click the "Register" button for Calculus 2
            _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div[2]/div[2]/div/div[2]/div/a[2]")).Click();
            //Click "yes" for the popup dialog box
            _driver.SwitchTo().Alert().Accept();
            Assert.DoesNotContain("Calculus 2", _driver.PageSource);

            //Click the "jb" buttom
            _driver.FindElement(By.LinkText("jb")).Click();
            Assert.Contains("Calculus 2", _driver.PageSource);

            //Click the "Drop" button for Calculus 2
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[7]/div[2]/div/div[2]/div/button")).Click();
            //Click "yes" for the popup dialog boxes
            _driver.SwitchTo().Alert().Accept();
            _driver.SwitchTo().Alert().Accept();

            //Ensure that "Calculus 2" is no displayed as Registered
            Assert.DoesNotContain("Calculus 2", _driver.PageSource);

            //Click the "Find Course" Button
            _driver.FindElement(By.LinkText("Find Course")).Click();
            Assert.Contains("Calculus 2", _driver.PageSource);

            signout();
        }
    }
}
