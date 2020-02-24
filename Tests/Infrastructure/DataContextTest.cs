using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Tests.Infrastructure
{
    public class DataContextTest : DataContext
    {
        public DataContextTest(DbContextOptions<DataContextTest> options) : base(options)
        {

        }

        public override void seed(ModelBuilder modelBuilder)
        {
            //override real data context's data seeding
            modelBuilder.Entity<Course>().HasData(
                new Course { courseId = 1, courseName = "COMP 4380", courseDescription = "Database Implementation", creditHours = 3, space = 80, date = "2019 Winter" },
                new Course { courseId = 2, courseName = "COMP 4350", courseDescription = "Software Engineering", creditHours = 3, space = 80, date = "2019 Winter" },
                new Course { courseId = 3, courseName = "COMP 4490", courseDescription = "Computer Graphics", creditHours = 3, space = 80, date = "2019 Winter" },
                new Course { courseId = 4, courseName = "COMP 4360", courseDescription = "Machine Learning", creditHours = 3, space = 80, date = "2019 Winter" }
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
                new Student { studentId = 1, name = "John Braico", majorId = 1, password = "password" },
                new Student { studentId = 2, name = "Mike Zapp", majorId = 2, password = "password" },
                new Student { studentId = 3, name = "Peter Graham", majorId = 3, password = "password" }
                );

            modelBuilder.Entity<Faculty>().HasData(
                new Faculty { facultyId = 1, facultyName = "Computer Science" },
                new Faculty { facultyId = 2, facultyName = "Engineering" },
                new Faculty { facultyId = 3, facultyName = "Arts" }
                );
        }
    }
}
