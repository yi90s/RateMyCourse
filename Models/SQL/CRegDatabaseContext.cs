using cReg_WebApp.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.SQL
{
    public class CRegCourseContext : DbContext
    {
        public CRegCourseContext(DbContextOptions<CRegCourseContext> options) : base(options) { }

        public DbSet<Course> Course { get; set; }

        public DbSet<Student> Student { get; set; }

    }
}
