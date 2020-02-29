using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.context
{
    public class 
        DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Prerequisite> Prerequisites { get; set; }

        public DbSet<Required> Required { get; set; }

        public DbSet<Enrolled> Enrolled { get; set; }

        public DbSet<StudentUser> StuentUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Required>()
                .HasKey(r => new { r.facultyId, r.courseId });

            modelBuilder.Entity<Prerequisite>()
                .HasKey(p => new { p.courseId, p.prerequisiteId });


            seed(modelBuilder);


        }

        public virtual void seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
            new Course { courseId = 1, courseName = "COMP 4380", courseDescription = "Database Implementation", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 2, courseName = "COMP 4350", courseDescription = "Software Engineering", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 3, courseName = "COMP 4490", courseDescription = "Computer Graphics", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
            new Course { courseId = 4, courseName = "COMP 4360", courseDescription = "Machine Learning", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) }
            );

            modelBuilder.Entity<Enrolled>().HasData(
                //John Brico's enrollments
                new Enrolled { enrollId = 1, studentId = 1, courseId = 1, completed = false, grade = -1, rating = -1, comment = "" },
                new Enrolled { enrollId = 2, studentId = 1, courseId = 2, completed = true, grade = 80, rating = 80, comment = "Very good" },
                //Mike Zapp's enrollments
                new Enrolled { enrollId = 3, studentId = 2, courseId = 1, completed = false, grade = -1, rating = -1, comment = "" },
                new Enrolled { enrollId = 4, studentId = 2, courseId = 3, completed = true, grade = 50, rating = -1, comment = "" },
                //Peter Graham's enrollements
                new Enrolled { enrollId = 5, studentId = 3, courseId = 2, completed = false, grade = -1, rating = -1, comment = "" },
                new Enrolled { enrollId = 6, studentId = 3, courseId = 4, completed = true, grade = 70, rating = 90, comment = "Excellent" }
                );

            modelBuilder.Entity<Student>().HasData(
                new Student { studentId = 1, name = "John Braico", majorId = 1},
                new Student { studentId = 2, name = "Mike Zapp", majorId = 2},
                new Student { studentId = 3, name = "Peter Graham", majorId = 3}
                );

            modelBuilder.Entity<StudentUser>().HasData(
                new StudentUser { UserName = "jb", StudentId = 1, Password = "password" },
                new StudentUser { UserName = "mz", StudentId = 2, Password = "password" },
                new StudentUser { UserName = "pg", StudentId = 3, Password = "password" }
                );

            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { facultyId = 1, facultyName = "Computer Science" },
                new Faculty { facultyId = 2, facultyName = "Engineering" },
                new Faculty { facultyId = 3, facultyName = "Arts" }
                );
        }


    }
}
