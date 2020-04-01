using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System.Linq;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class BuildFindCourseViewModelAsyncTests : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildFindCourseViewModelAsyncTests()
        {
            _viewModelService = new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService

        //buildFindCourseViewModelTestAsync
        [Fact]
        public async void buildFindCourseViewModelTestAsync_HappyPath()
        {
            Student student = _context.Students.Find(1);
            FindCourseViewModel findCourseViewModel = await _viewModelService.buildFindCourseViewModelAsync(student);
            Assert.True(findCourseViewModel.courseList.First().courseId == 12);
            Assert.True(findCourseViewModel.courseList.Count() == 8);
        }

        [Fact]
        public async void buildFindCourseViewModelTestAsync_NullParameter()
        {
            FindCourseViewModel test = await _viewModelService.buildFindCourseViewModelAsync(null);
            Assert.Null(test);
        }
    }
}