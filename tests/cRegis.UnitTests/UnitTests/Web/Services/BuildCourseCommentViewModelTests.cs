

using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class BuildCourseCommentViewModelTests : ViewModelServiceTestsBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildCourseCommentViewModelTests()
        {
            _viewModelService = getContext();
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService

        //buildCourseCommentViewModelTest
        [Fact]
        public void buildCourseCommentViewModelTest_HappyPath()
        {
            Enrolled tempEnrolled = _context.Enrolled.Find(1);
            tempEnrolled.course = _context.Courses.Find(tempEnrolled.courseId);
            Comment testComment = new Comment(tempEnrolled);
            CourseCommentViewModel courseCommentViewModel = _viewModelService.buildCourseCommentViewModel(testComment);
            Assert.True(courseCommentViewModel.ratingScore == 90, "rating should be 90");
            Assert.Equal("I like that course", courseCommentViewModel.comment);
            Assert.True(courseCommentViewModel.takenDate.Equals(new DateTime(2019, 9, 6)), "should have same date");
        }

        [Fact]
        public void buildCourseCommentViewModelTest_NullParameter()
        {
            Comment testComment = null;
            CourseCommentViewModel courseCommentViewModel = _viewModelService.buildCourseCommentViewModel(testComment);
            Assert.Null(courseCommentViewModel);
        }
    }
}