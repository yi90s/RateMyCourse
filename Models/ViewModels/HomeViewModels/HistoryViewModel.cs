using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels.CourseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class HistoryViewModel
    {
        public Student thisStudent { get; set; }
        public IEnumerable<CourseContainerViewModel> courses { get; set; }

        public HistoryViewModel(Student thisStudent, IEnumerable<CourseContainerViewModel> courses)
        {
            this.thisStudent = thisStudent;
            this.courses = courses;
        }
    }
}
