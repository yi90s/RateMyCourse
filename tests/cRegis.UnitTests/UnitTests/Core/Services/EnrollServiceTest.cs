using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{

    public class EnrollServiceTest : TestBase
    {
        private readonly IEnrollService _enrollService;
        public EnrollServiceTest()
        {
            _enrollService = new EnrollService(_context);
        }

        [Fact]
        public async void nullFindEnrollById()
        {
            Enrolled c = await _enrollService.getEnrollAsync(-1);
            Assert.Null(c);
        }

        //TODO : writting test against all the methods in cRegis.Core.Service.EnrollService
    }
}
