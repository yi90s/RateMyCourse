
using cReg_WebApp.Models.context;
using cReg_WebApp.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.test.Infrastructure
{
    public class TestBase : IDisposable
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

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }
    }
}
