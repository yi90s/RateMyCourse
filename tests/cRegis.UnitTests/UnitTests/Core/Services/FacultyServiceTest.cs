using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{
    public class FacultyServiceTest : TestBase
    {
        private readonly IFacultyService _facultyService;
        public FacultyServiceTest()
        {
            _facultyService = new FacultyService(_context);
        }

        [Fact]
        public void nullFindFacultyById()
        {
            Faculty c = _facultyService.getFaculty(-1);
            Assert.Null(c);
        }

        //TODO : writting test against all the methods in cRegis.Core.Service.FacultyService

        public async void getFacultyTest()
        {
            Faculty faculty1 = await _facultyService.getFaculty(1);
            Faculty faculty2 = await _facultyService.getFaculty(2);
            Assert.NotNull(faculty1);
            Assert.NotNull(faculty2);
        }
    }
}
