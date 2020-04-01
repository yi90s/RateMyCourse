using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class BuildRateCourseViewModelTests : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildRateCourseViewModelTests()
        {
            _viewModelService = new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService
        //buildRateCourseViewModelTest
        [Fact]
        public void buildRateCourseViewModelTest_HappyPath()
        {
            Enrolled enrolled = _context.Enrolled.Find(9);
            Course course = _context.Courses.Find(enrolled.courseId);
            RateCourseViewModel rateCourseViewModel = _viewModelService.buildRateCourseViewModel(enrolled, course);
            Assert.True(rateCourseViewModel.EnrollId == 9);
            Assert.Null(rateCourseViewModel.Rating);
            Assert.Null(rateCourseViewModel.Comment);
            Assert.Equal("COMP 3350", rateCourseViewModel.CourseName);
            Assert.Equal("Software Engineer 1", rateCourseViewModel.courseDescription);
        }

        [Fact]
        public void buildRateCourseViewModelTest_NullEnrolled()
        {
            Course course = _context.Courses.Find(_context.Enrolled.Find(9).courseId);
            RateCourseViewModel rateCourseViewModel = _viewModelService.buildRateCourseViewModel(null, course);
            Assert.Null(rateCourseViewModel);
        }

        [Fact]
        public void buildRateCourseViewModelTest_NullCourse()
        {
            Enrolled enrolled = _context.Enrolled.Find(9);
            RateCourseViewModel rateCourseViewModel = _viewModelService.buildRateCourseViewModel(enrolled, null);
            Assert.Null(rateCourseViewModel);
        }
    }
}