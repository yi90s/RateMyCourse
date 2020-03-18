//using cRegis.Web.Models;
//using cRegis.Web.Models.context;
//using cRegis.Web.Models.entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace cRegis.Web.Tests.Infrastructure
//{
//    public class DataContextTest : DataContext
//    {
//        public DataContextTest(DbContextOptions<DataContextTest> options) : base(options)
//        {

//        }

//        protected override void seed(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Course>().HasData(
//            new Course { courseId = 1, courseName = "COMP 4380", courseDescription = "Database Implementation", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
//            new Course { courseId = 2, courseName = "COMP 4350", courseDescription = "Software Engineering", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
//            new Course { courseId = 3, courseName = "COMP 4490", courseDescription = "Computer Graphics", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) },
//            new Course { courseId = 4, courseName = "COMP 4360", courseDescription = "Machine Learning", creditHours = 3, space = 80, date = new DateTime(2019, 9, 6) }
//            );

//            modelBuilder.Entity<Enrolled>().HasData(
//                //John Brico's enrollments
//                new Enrolled { enrollId = 1, studentId = 1, courseId = 1, completed = false, grade = null, rating = null, comment = null },
//                new Enrolled { enrollId = 2, studentId = 1, courseId = 2, completed = true, grade = 80, rating = 80, comment = "Very good" },
//                //Mike Zapp's enrollments
//                new Enrolled { enrollId = 3, studentId = 2, courseId = 1, completed = false, grade = null, rating = null, comment = null },
//                new Enrolled { enrollId = 4, studentId = 2, courseId = 3, completed = true, grade = 50, rating = null, comment = null },
//                //Peter Graham's enrollements
//                new Enrolled { enrollId = 5, studentId = 3, courseId = 2, completed = false, grade = null, rating = null, comment = null },
//                new Enrolled { enrollId = 6, studentId = 3, courseId = 4, completed = true, grade = 70, rating = 90, comment = "Excellent" }
//                );

//            modelBuilder.Entity<Student>().HasData(
//                new Student { studentId = 1, name = "John Braico", majorId = 1 },
//                new Student { studentId = 2, name = "Mike Zapp", majorId = 2 },
//                new Student { studentId = 3, name = "Peter Graham", majorId = 3 }
//                );


//            modelBuilder.Entity<Faculty>().HasData(
//                new Faculty { facultyId = 1, facultyName = "Computer Science" },
//                new Faculty { facultyId = 2, facultyName = "Engineering" },
//                new Faculty { facultyId = 3, facultyName = "Arts" }
//                );

//            modelBuilder.Entity<IdentityRole>().HasData(
//                new IdentityRole { Name = "Student", NormalizedName = "STUDENT" },
//                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
//                );

//            modelBuilder.Entity<StudentUser>().HasData(
//                makeStudentUser("jb", 1, "Password1!"),
//                makeStudentUser("mz", 2, "Password1!"),
//                makeStudentUser("pg", 3, "Password1!")
//                );
//        }
//    }
//}
