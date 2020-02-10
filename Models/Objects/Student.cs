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

        public Boolean addCourseToShortlist(Course course)
        {
            //To prevent duplicates
            Boolean result = false;
            foreach (var cor in shortlist)
            {
                if (cor.id == course.id)
                {
                    shortlist.Remove(cor);
                    result = true;
                    break;
                }
            }
            shortlist.Add(course);
            return result;
        }

        public List<Course> getShortlist()
        {
            return shortlist;
        }
    }
}
