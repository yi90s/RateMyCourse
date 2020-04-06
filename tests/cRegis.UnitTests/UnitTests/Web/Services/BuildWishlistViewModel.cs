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
    public class BuildWishlistViewModelTests : ViewModelServiceTestsBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildWishlistViewModelTests()
        {
            _viewModelService = getContext();
        }
        [Fact]
        public void buildWishlistViewModelTest_HappyPath()
        {
            Student thisStudent = _context.Students.Find(1);
            WishlistViewModel test = _viewModelService.buildWishlistViewModel(thisStudent);
            Assert.True(test.courses.First().courseId == 1);
            Assert.True(test.courses.Count() == 5);
        }

        [Fact]
        public void buildWishlistViewModelEdgeCaseTest_NullParameter()
        {
            WishlistViewModel test = _viewModelService.buildWishlistViewModel(null);
            Assert.Null(test);
        }
    }
}