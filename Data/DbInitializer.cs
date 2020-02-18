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
                new Student{Name="Carson", MajorId=1, Password="123456"}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseName="Database Implementation"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var faculties = new Faculty[]
            {
                new Faculty{FacultyId=1,FacultyName="Computer Science"},
                new Faculty{FacultyId=2,FacultyName="Engineering"}
            };
            foreach (Faculty faculty in faculties)
            {
                context.Add(faculty);
            }

        }
    }
}
