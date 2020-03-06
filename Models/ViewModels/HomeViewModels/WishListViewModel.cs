using cReg_WebApp.Models.entities;
using System.Collections.Generic;

namespace cReg_WebApp.Models.ViewModels.HomeViewModels
{
    public class WishListViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public List<Course> courseList { get;  }

        public WishListViewModel(Student thisStudent, string majorName, List<Course> list)
        {
            this.thisStudent = thisStudent;
            this.majorName = majorName;
            this.courseList = list;
        }
    }
}
