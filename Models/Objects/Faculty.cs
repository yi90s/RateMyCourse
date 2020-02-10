using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Faculty
    {
        public String name { get; }
        public int id { get; }
        private List<Course> courseSet = new List<Course>();
        private List<Student> studentSet = new List<Student>();

        public Faculty(String name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Boolean addToCourseSet(Course course)
        {
            //To prevent duplicates
            Boolean result = false;
            foreach (var cor in courseSet)
            {
                if (cor.id == course.id)
                {
                    courseSet.Remove(cor);
                    result = true;
                    break;
                }
            }
            courseSet.Add(course);
            return result;
        }

        public void removeFromCourseSet(Course course)
        {
            courseSet.Remove(course);
        }

        public Boolean addStudentToFaculty(Student student)
        {
            //To prevent duplicates
            Boolean result = false;
            foreach (var stu in studentSet)
            {
                if (stu.id == student.id)
                {
                    studentSet.Remove(stu);
                    result = true;
                    break;
                }
            }
            studentSet.Add(student);
            return result;
        }

        public void removeStudentFromFaculty(Student student)
        {
            studentSet.Remove(student);
        }
    }
}