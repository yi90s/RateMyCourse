using cRegis.API.Controllers;
using cRegis.Core.Entities;
using cRegis.UnitTests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace cRegis.UnitTests.UnitTests.API
{
    public class FacultyControllerTests:APITestBase
    {
        private FacultyController _controller;
        public FacultyControllerTests():base()
        {
            _controller = new FacultyController(_facultyService);
        }

        [Theory]
        [InlineData(1)]
        public void getGacultyTest(int fid)
        {
            var result = _controller.getFaculty(fid);
            Assert.NotNull(result);
            Assert.IsType<Faculty>(result.Value);
        }
    }
}
