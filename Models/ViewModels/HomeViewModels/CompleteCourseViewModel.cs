using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class CompleteCourseViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public Dictionary<int, Course> keyValues { get; }

        public CompleteCourseViewModel(Student thisStudent, string majorName, Dictionary<int, Course> keyValues)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            this.keyValues = keyValues;
        }
    }
}
