using cReg_WebApp.Models.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Prerequisite> Prerequisites { get; set; }

        public DbSet<Required> Required { get; set; }

        public DbSet<Enrolled> Enrolled { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Required>()
                .HasKey(r => new { r.facultyId, r.courseId });

            modelBuilder.Entity<Prerequisite>()
                .HasKey(p => new { p.courseId, p.prerequisiteId });

        }
    }
}
