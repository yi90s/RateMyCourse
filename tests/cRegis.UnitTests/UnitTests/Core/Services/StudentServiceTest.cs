using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{
    public class StudentServiceTest : TestBase
    {
        private readonly IStudentService _studentService;

        public StudentServiceTest()
        {
            _studentService = new StudentService(_context);
        }

        [Fact]
        public async void nullFindStudentById()
        {
            Student c = await _studentService.getStudentAsync(-1);
            Assert.Null(c);
        }

        //TODO : writting test against all the methods in cRegis.Core.Service.StudentService

    }
}
