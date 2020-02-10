using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Student
    {
        public String name { get; }
        public int id { get; }
        public Faculty major { get; set; }
        public Faculty minor { get; set; }
        List<Course> shortlist = new List<Course>();

        public Student (String name, int id)
        {
            this.name = name;
            this.id = id;
        }
        public Student (String name, int id, Faculty major, Faculty minor)
        {
            this.name = name;
            this.id = id;
            this.major = major;
            this.minor = minor;
        }

        void addCourseToShortlist(Course course)
        {
            shortlist.Add(course);
        }

        List<Course> getShortlist()
        {
            return shortlist;
        }
    }
}
