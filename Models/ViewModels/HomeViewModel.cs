using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class HomeViewModel
    {
        public string name;

        public List<Enrolled> takingCourses;
        public HomeViewModel(List<Enrolled> takingCourses, Student studentInfo)
        {
             
        }
    }
}
