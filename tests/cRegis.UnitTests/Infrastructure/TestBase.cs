
//using cRegis.Web.Models;
//using cRegis.Web.Models.entities;
//using cRegis.Web.Tests.Infrastructure;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System;

//namespace cRegis.Web.test.Infrastructure
//{
//    public class TestBase : IDisposable
//    {
//        protected readonly DataContextTest _context;
//        protected readonly UserManager<StudentUser> mockUserManager;
//        protected readonly SignInManager<StudentUser> mockSignInManager;

//        public TestBase()
//        {
//            var userStore = new Mock<IUserStore<StudentUser>>();
//            var options = new DbContextOptionsBuilder<DataContextTest>()
//                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//                .Options;

//            mockUserManager = new UserManager<StudentUser>(userStore.Object, null, null, null, null, null, null, null, null);
//            mockSignInManager = new SignInManager<StudentUser>(mockUserManager, null, null, null, null, null, null);

//            _context = new DataContextTest(options);
//            _context.Database.EnsureCreated();

//        }

//        public void Dispose()
//        {
//            _context.Database.EnsureDeleted();

//            _context.Dispose();
//        }
//    }
//}
