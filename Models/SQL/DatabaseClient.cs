using cReg_WebApp.Models.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace cReg_WebApp.Models.SQL
{
    public static class DatabaseClient
    {
        // TODO: Figure out how to not hard code the connection string, etc. into here
        public static IServiceProvider _serviceProvider;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            using (var context = new CRegCourseContext(
                _serviceProvider.GetRequiredService<DbContextOptions<CRegCourseContext>>()))
            {
                if (context.Course.Any())
                {
                    return;
                }

                context.Course.AddRange(
                    new Course
                    {
                        Id = 2,
                        SectionId = "A01",
                        Name = "COMP 4350",
                        Description = "Software Engineering 2"
                    });
            }
        }

        public static void InsertCourseIntoTable(Course course)
        {
            using (var context = new CRegCourseContext(
                   _serviceProvider.GetRequiredService<DbContextOptions<CRegCourseContext>>()))
            {
                context.Add(course);
            }
        }
    }
}
