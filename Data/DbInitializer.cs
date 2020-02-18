using cReg_WebApp.Models.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cReg_WebApp.Models.entities;


namespace cReg_WebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();


            var students = new Student[]
            {
                new Student{name="Carson", majorId=1, password="123456", studentId=1}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{courseId=1, courseName="Database Implementation"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var faculties = new Faculty[]
            {
                new Faculty{facultyId=1,facultyName="Computer Science"},
                new Faculty{facultyId=2,facultyName="Engineering"}
            };
            foreach (Faculty faculty in faculties)
            {
                context.Add(faculty);
            }


        }
    }
}
