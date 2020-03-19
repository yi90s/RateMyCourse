//using cRegis.Web.Controllers;
//using cRegis.Web.Models.entities;
//using cRegis.Web.Models.ViewModels;
//using cRegis.Web.test.Infrastructure;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Identity;

//namespace cRegis.Web.test.UnitTests.Controller
//{
//    // This class contain all tests for Home Controller
//    public class HomeControllerTests : TestBase
//    {
//        private HomeController controller;

//        public HomeControllerTests()
//        {
//            controller = new HomeController(_context, mockUserManager, mockSignInManager, mockHasher);
//        }


//        [Fact]
//        public async Task PostLogin_ReturnsHomePage()
//        {

//            // Arrange 
//            //var controller = new HomeController(_context, mockUserManager, mockSignInManager);

//            LoginViewModel viewModel = new LoginViewModel { UserName = "jb", Password = "Password!" };
//            var result = (RedirectToActionResult)await controller.Login(viewModel);
//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Index", result.ActionName);
//            Assert.Equal("Home", result.ControllerName);

//        }
//        [Fact]
//        public async Task GetLogin_ReturnsLoginPage()
//        {
//            // Arrange 
//            //var controller = new HomeController(_context);


//            LoginViewModel viewModel = new LoginViewModel { UserName = "jb", Password = "Password!" };
//            ViewResult result = (ViewResult)await controller.Login(viewModel);

//            // Assert 
//            Assert.NotNull(result);
//            Assert.Equal("Login", result.ViewName);

//        }

//        [Fact]
//        public async Task GetIndex_ReturnsCorrectCourseList()
//        {
//            // Arrange 
//            //var controller = new HomeController(_context);

//            // Act 
//            int STUDENT_ID = 1;
//            ViewResult result = (ViewResult)await controller.Index(STUDENT_ID);
//            List<Course> registeredCourses = (List<Course>)result.Model;
//            // Assert 
//            Assert.NotNull(result);
//            Assert.Equal(1, registeredCourses[0].courseId);
//        }

//        [Fact]
//        public async Task GetRegister_ReturnsCorrectCourseList()
//        {
//            // Arrange 
//            //var controller = new HomeController(_context);

//            // Act 
//            int STUDENT_ID = 1;
//            ViewResult result = (ViewResult)await controller.Register(STUDENT_ID);
//            List<Course> registeredCourses = (List<Course>)result.Model;
//            var expectedIds = new List<int>() { 2, 3, 4 };

//            // Assert 
//            Assert.NotNull(result);
//            Assert.Equal(expectedIds.Count, registeredCourses.Count);
//            registeredCourses.ForEach(c => expectedIds.Remove(c.courseId));
//            Assert.Empty(expectedIds);
//        }


//        [Fact]
//        public async Task GetRateCourse_ReturnsCorrectModel()
//        {
//            // Arrange 
//            //var controller = new HomeController(_context);

//            // Act (enrollId = 1)
//            ViewResult result = (ViewResult)await controller.RateCourse(1);
//            RateCourseViewModel model = (RateCourseViewModel)result.Model;

//            // Assert 
//            Assert.Equal(1, model.EnrollId);
//            Assert.Equal(-1, model.Rating);
//            Assert.Equal("", model.Comment);
//            Assert.Equal("COMP 4380 - Database Implementation", model.CourseFullName);

//        }

//        [Fact]
//        public async Task PostUpdateCourseRate_RateIsUpdated()
//        {
//            //var controller = new HomeController(_context);
//            Enrolled rate = _context.Enrolled.Find(4);
//            Course course = _context.Courses.Find(rate.courseId);

//            var newCourseRateViewModel = new RateCourseViewModel(rate, course);
//            newCourseRateViewModel.Comment = "Testing comments";
//            newCourseRateViewModel.Rating = 99;
//            await controller.UpdateCourseRate(newCourseRateViewModel);

//            Enrolled newRate = _context.Enrolled.Find(4);
//            Assert.Equal(rate.enrollId, newRate.enrollId);
//            Assert.Equal(rate.studentId, newRate.studentId);
//            Assert.Equal(rate.completed, newRate.completed);
//            Assert.Equal(rate.grade, newRate.grade);
//            Assert.Equal(99, newRate.rating);
//            Assert.Equal("Testing comments", newRate.comment);
//        }


//        [Fact]
//        public async Task GetComplete_ReturnsCorrectCourseList()
//        {
//            // Arrange 
//            //var controller = new HomeController(_context);

//            // Act 
//            int STUDENT_ID = 1;
//            ViewResult result = (ViewResult)await controller.Complete(STUDENT_ID);
//            List<Course> completedCourses = (List<Course>)result.Model;
//            // Assert 
//            Assert.NotNull(result);
//            Assert.Equal(2, completedCourses[0].courseId);
//        }
//    }
//}
