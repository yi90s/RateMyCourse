using cReg_WebApp.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace cReg_WebApp.Models.SQL
{
    public class CRegCourseContext : DbContext
    {
        public CRegCourseContext(DbContextOptions<CRegCourseContext> options)
            : base(options) { }

        public DbSet<Course> Course { get; set; }

    }
}
