using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;

namespace cReg_WebApp.Models.ViewModels.HomeViewModels
{
    public class FindCourseViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public List<entities.Course> courseList { get; }

        public FindCourseViewModel(Student thisStudent, string majorName, List<entities.Course> list)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            this.courseList = list;
        }
    }
}
