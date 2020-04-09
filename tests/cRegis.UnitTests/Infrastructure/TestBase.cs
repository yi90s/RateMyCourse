using cRegis.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace cRegis.Web.test.Infrastructure
{
    public class TestBase
    {
        protected readonly DataContextTest _context;


        public TestBase()
        {
            var options = new DbContextOptionsBuilder<DataContextTest>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContextTest(options);
            _context.Database.EnsureCreated();

        }

    }
}
