using System;
using System.Collections.Generic;

namespace cReg_WebApp.Models.Objects
{
    public class Faculty
    {
        public string name { get; }
        public int id { get; }
        //TODO make these into classes
        CourseList courseList = null;
        StudentList studentList = null;

        public Faculty(string name, int id)
        {
            this.name = name;
            this.id = id;
            this.courseList = new CourseList(id);
            this.studentList = new StudentList(id);
        }

        public bool AddToCourseList(Course course)
        {
            return courseList.AddToCourseList(course);
        }

        public void RemoveFromCourseList(Course course)
        {
            courseList.RemoveFromCourseList(course);
        }

        public List<Course> GetCoursesOffered()
        {
            return courseList.GetCoursesOffered();
        }

        public bool AddStudentToFaculty(Student student)
        {
            return studentList.AddStudentToFaculty(student);
        }

        public void RemoveStudentFromFaculty(Student student)
        {
            studentList.RemoveStudentFromFaculty(student);
        }

        public List<Student> GetStudents()
        {
            return studentList.GetStudents();
        }
    }
}