using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers.Logic
{
    public class Shortlist
    {
        private Student student;
        private List<Course> courseList = new List<Course>();

        public Shortlist (Student student)
        {
            this.student = student;
        }

        public void addToShortlist(Course course)
        {
            courseList.Add(course);
        }

        public List<Course> getShortlist()
        {
            return courseList;
        }
    }
}
