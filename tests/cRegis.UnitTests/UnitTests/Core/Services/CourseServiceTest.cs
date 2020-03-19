using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using Xunit;

namespace cRegis.UnitTests.Core.Services
{
    public class CourseServiceTest : TestBase
    {
        private readonly ICourseService _courseService;
        public CourseServiceTest()
        {
            _courseService = new CourseService(_context);
        }

        [Fact]
        public void nullFindCourseById()
        {
            Course c = _courseService.getCourse(-1);
            Assert.Null(c);
        }

        //TODO : writting test against all the methods in cRegis.Core.Service.CourseService

    }
}
