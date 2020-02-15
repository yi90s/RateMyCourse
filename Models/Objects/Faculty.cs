using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cReg_WebApp.Models.Objects
{
    public class Faculty
    {
        public int Id { get; }
        public string Name { get; }
        //TODO make these into classes
        CourseList CourseList = null;
        StudentList StudentList = null;

        public Faculty() { }

        public Faculty(string name, int id)
        {
            Name = name;
            Id = id;
            CourseList = new CourseList(id);
            StudentList = new StudentList(id);
        }

        public bool AddToCourseList(Course course)
        {
            return CourseList.AddToCourseList(course);
        }

        public void RemoveFromCourseList(Course course)
        {
            CourseList.RemoveFromCourseList(course);
        }

        public List<Course> GetCoursesOffered()
        {
            return CourseList.GetCoursesOffered();
        }

        public bool AddStudentToFaculty(Student student)
        {
            return StudentList.AddStudentToFaculty(student);
        }

        public void RemoveStudentFromFaculty(Student student)
        {
            StudentList.RemoveStudentFromFaculty(student);
        }

        public List<Student> GetStudents()
        {
            return StudentList.GetStudents();
        }
    }
}