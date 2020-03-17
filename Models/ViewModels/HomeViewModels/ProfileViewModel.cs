using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }

        public int remainningCreditHours { get; set; }
        public Dictionary<int, entities.Course> keyValues { get; }

        public ProfileViewModel(Student thisStudent, string majorName, int creditHours, Dictionary<int, entities.Course> keyValues)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            remainningCreditHours = creditHours;
            this.keyValues = keyValues;
        }
    }
}
