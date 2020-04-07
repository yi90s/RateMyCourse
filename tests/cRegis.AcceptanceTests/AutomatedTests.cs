using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace cRegis.AcceptanceTests
{
    public class AutomatedTests : IDisposable
    {
        private Random rand = new Random();
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

        //Tests

        [Fact]
        public void Signin_StudentInfo_Signout_Test()
        {
            signin();

            //Verify that the student info is displayed (ie. signin worked)
            Assert.Contains("Welcome, John Braico", _driver.PageSource);
            Assert.Contains("Student Name : John Braico", _driver.PageSource);
            Assert.Contains("Student Id : 1", _driver.PageSource);
            Assert.Contains("Major : Computer Science", _driver.PageSource);
            Assert.Contains("Credit hours remaining : 24", _driver.PageSource);

            signout();

            //Verify that the Signout occurs correctly
            Assert.DoesNotContain("Welcome, John Braico", _driver.PageSource);
        }

        [Fact]
        public void FindCourse_RegisterCourse_DropCourse_Test()
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

        [Fact]
        public void FindCourse_ViewDetails_Test()
        {
            signin();

            //Click the "Details" button for "Comp 3370 - Computer Organization"
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[2]/div[1]/div[2]/div/div[2]/div/a")).Click();

            //Verify that it shows the details for that course
            Assert.Contains("COMP 3370 - Computer Organization", _driver.PageSource);
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);

            signout();
        }

        [Fact]
        public void History_ViewDetails_Test()
        {
            signin();

            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains("Registration History", _driver.PageSource);
            Assert.Contains("An Introduction to Computer Science 1", _driver.PageSource);

            //Click the "Detail" button for this course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[1]")).Click();

            //Verify that it shows the details for that course
            Assert.Contains("COMP 1010 - An Introduction to Computer Science 1", _driver.PageSource);
            Assert.Contains("Available Space", _driver.PageSource);
            Assert.Contains("Date", _driver.PageSource);
            Assert.Contains("Average Rating", _driver.PageSource);
            Assert.Contains("Instructor", _driver.PageSource);
            Assert.Contains("Location", _driver.PageSource);

            //Verify that it shows the rating for that course
            Assert.Contains("NEVER AGAIN", _driver.PageSource);

            signout();
        }

        [Fact]
        public void History_Rate_Test()
        {
            string rating = rand.Next(100).ToString();
            string randomComment = rand.Next().ToString();

            signin();

            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains("Registration History", _driver.PageSource);
            Assert.Contains("An Introduction to Computer Science 1", _driver.PageSource);

            //Click the "Rate" button for this course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[2]")).Click();
            Assert.Contains("Your Rating(0-100)", _driver.PageSource);
            Assert.Contains("Your Comment", _driver.PageSource);

            //Enter known random input for rating
            _driver.FindElement(By.Id("Rating")).Clear();
            _driver.FindElement(By.Id("Rating")).SendKeys(rating);
            _driver.FindElement(By.Id("Comment")).Clear();
            _driver.FindElement(By.Id("Comment")).SendKeys(randomComment);
            _driver.FindElement(By.XPath("/html/body/div/main/div/form/div[3]/input")).Click();

            //Click the "History" Button
            _driver.FindElement(By.LinkText("History")).Click();
            Assert.Contains("An Introduction to Computer Science 1", _driver.PageSource);

            //Click the "Detail" button for this course
            _driver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/div/div[2]/div/a[1]")).Click();

            //Verify that the rating was added
            Assert.Contains(rating, _driver.PageSource);
            Assert.Contains(randomComment, _driver.PageSource);

            signout();
        }
    }
}
