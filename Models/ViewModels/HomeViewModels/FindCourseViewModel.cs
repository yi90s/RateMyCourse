using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;

namespace cReg_WebApp.Models.ViewModels.HomeViewModels
{
    public class FindCourseViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public List<Course> courseList { get; }

        public FindCourseViewModel(Student thisStudent, string majorName, List<Course> list)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            this.courseList = list;
        }
    }
}
