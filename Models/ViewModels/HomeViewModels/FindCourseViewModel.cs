using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels.CourseViewModels;
using System;
using System.Collections.Generic;

namespace cReg_WebApp.Models.ViewModels.HomeViewModels
{
    public class FindCourseViewModel
    {
        public List<CourseContainerViewModel> courseList { get; }

        public FindCourseViewModel(List<CourseContainerViewModel> list)
        {
            this.courseList = list;
        }
    }
}
