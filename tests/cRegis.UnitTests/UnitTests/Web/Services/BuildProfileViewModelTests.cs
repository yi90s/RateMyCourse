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
    public class BuildProfileViewModelTests : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildProfileViewModelTests()
        {
            _viewModelService = new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService
        //buildProfileViewModelTestAsync
        [Fact]
        public void buildProfileViewModelTest_HappyPath()
        {
            Student student = _context.Students.Find(1);
            ProfileViewModel profileViewModel = _viewModelService.buildProfileViewModel(student);
            Assert.True(profileViewModel.thisStudent.studentId == 1, "student id should equal 1");
            Assert.Equal("John Braico", profileViewModel.thisStudent.name);
            Assert.Equal("Computer Science", profileViewModel.majorName);
            Assert.True(profileViewModel.cViewModels.Count() == 0, "student should have 0 erolls");
            Assert.True(profileViewModel.remainingCreditHours == 24, "student should have 24 credit hours remaining");
        }

        [Fact]
        public void buildProfileViewModelTest_NullParameter()
        {
            ProfileViewModel result = _viewModelService.buildProfileViewModel(null);
            Assert.Null(result);
        }
    }
}