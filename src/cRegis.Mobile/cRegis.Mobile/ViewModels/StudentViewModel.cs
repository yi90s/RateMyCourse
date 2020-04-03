using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.ViewModels
{
    class StudentViewModel
    {

        public string studentName { get; set; }
        public int studentID { get; set; }

        public string facultyName { get; set; }

        public string creditRemain { get; set; }

        public List<Course> enrolledlist { get; set; }

        public void test()
        {
            studentID = 1;
            studentName = "John Braico";
            facultyName = "Computer Science";
            creditRemain = "120";
            enrolledlist = new List<Course>();
            enrolledlist.Add(new Course() { courseName = "COMP 4350", courseId = 1, courseDescription = "Software Engineering" });
            enrolledlist.Add(new Course() { courseName = "MATH 1500", courseId = 2, courseDescription = "Introduction to Calculus"});
        }

        public StudentViewModel()
        {
            test();
        }

        public StudentViewModel(Student s, string crehrs, List<Course> listC)
        {
            studentName = s.name;
            studentID = s.studentId;
            //facultyName = s.major.facultyName;
            creditRemain = crehrs;
            //creditRemain = "120";
            enrolledlist = listC;
        }

    }
}
