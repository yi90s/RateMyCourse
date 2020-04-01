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
    public class BuildHistoryViewModelTests : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildHistoryViewModelTests()
        {
            _viewModelService = new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService
        //buildHistoryViewModelTest
        [Fact]
        public void buildHistoryViewModelTest_HappyPath()
        {
            Student student = _context.Students.Find(1);
            HistoryViewModel historyViewModel = _viewModelService.buildHistoryViewModel(student);
            Assert.True(historyViewModel.courses.First().courseId == 1);
            Assert.True(historyViewModel.courses.Count() == 12);
        }

        [Fact]
        public void buildHistoryViewModelTest_NullParameter()
        {
            HistoryViewModel historyViewModel = _viewModelService.buildHistoryViewModel(null);
            Assert.Null(historyViewModel);
        }
    }
}