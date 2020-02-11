using System;
using System.Collections.Generic;

namespace cReg_WebApp.Models.Objects
{
    public class Faculty
    {
        public string name { get; }
        public int id { get; }
        private List<Course> courseSet = new List<Course>();
        private List<Student> studentSet = new List<Student>();

        public Faculty(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public bool AddToCourseSet(Course course)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var cor in courseSet) if (!result)
                {
                    if (cor.id == course.id)
                    {
                        courseSet.Remove(cor);
                        result = true;
                    }
                }
            courseSet.Add(course);
            return result;
        }

        public void RemoveFromCourseSet(Course course)
        {
            courseSet.Remove(course);
        }

        public List<Student> GetStudents()
        {
            return studentSet;
        }

        public List<Course> GetCoursesOffered()
        {
            return courseSet;
        }

        public bool AddStudentToFaculty(Student student)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var stu in studentSet) if (!result)
                {
                    if (stu.id == student.id)
                    {
                        studentSet.Remove(stu);
                        result = true;
                    }
                }
            studentSet.Add(student);
            return result;
        }

        public void RemoveStudentFromFaculty(Student student)
        {
            StudentSet.Remove(student);
        }
    }
}