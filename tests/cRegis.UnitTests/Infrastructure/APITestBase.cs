using cRegis.Core.Services;
using cRegis.Tests.Infrastructure.FakeStubs;
using cRegis.Web.test.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace cRegis.UnitTests.Infrastructure
{
    public class APITestBase:TestBase
    {
        protected readonly FakeUserManager _userManager;
        protected readonly FakeSignInManager _signInManager;
        protected readonly CourseService _courseService;
        protected readonly StudentService _studentService;
        protected readonly EnrollService _enrollService;
        protected readonly FacultyService _facultyService;
        protected readonly WishlistService _wishlistService;

        public APITestBase():base()
        {

            _userManager = new FakeUserManager(_context);

            _signInManager = new FakeSignInManager(_userManager);
            _courseService = new CourseService(_context);
            _studentService = new StudentService(_context);
            _enrollService = new EnrollService(_context);
            _facultyService = new FacultyService(_context);
            _wishlistService = new WishlistService(_context);

        }
    }
}
