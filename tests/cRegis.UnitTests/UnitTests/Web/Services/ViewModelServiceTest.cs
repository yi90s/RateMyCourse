

using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class ViewModelServiceTest : TestBase
    {
        private readonly IViewModelService _viewModelSerivce;

        public ViewModelServiceTest()
        {
            _viewModelSerivce = new ViewModelService(new CourseService(_context), 
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context),
                new WishlistService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService

        [Fact]
        public void normalBuildCourseCommentViewModel()
        {
            //TODO
        }

    }
}
