using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System.Linq;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class BuildCourseDetailViewModelTests : ViewModelServiceTestsBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildCourseDetailViewModelTests()
        {
            _viewModelService = getContext();
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService

        //buildCourseDetailViewModelTest
        [Fact]
        public void buildCourseDetailViewModelTest_HappyPath()
        {
            CourseDetailViewModel courseDetailViewModel = _viewModelService.buildCourseDetailViewModel(1);
            Assert.Equal("COMP 1010", courseDetailViewModel.courseName);
            Assert.Equal("An Introduction to Computer Science 1", courseDetailViewModel.courseDescription);
            Assert.True(courseDetailViewModel.availableSpace == 5);
            Assert.Equal("77/100", courseDetailViewModel.avgRating);
            Assert.True(courseDetailViewModel.comments.Count() == 7);
        }

        [Fact]
        public void buildCourseDetailViewModelTest_NonExistantCid()
        {
            CourseDetailViewModel courseDetailViewModel = _viewModelService.buildCourseDetailViewModel(-1);
            Assert.Null(courseDetailViewModel);
        }

        [Fact]
        public void buildCourseDetailViewModelTest_NoComments()
        {
            CourseDetailViewModel courseDetailViewModel = _viewModelService.buildCourseDetailViewModel(19);
            Assert.Equal("N/A", courseDetailViewModel.avgRating);
            Assert.Null(courseDetailViewModel.comments);
        }
    }
}