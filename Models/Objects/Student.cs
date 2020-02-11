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
        public int currYear { get; set; }
        List<Course> shortlist = new List<Course>();
        List<Course> completedCourses = new List<Course>();

        public Student (String name, int id, int currYear)
        {
            this.name = name;
            this.id = id;
            this.currYear = currYear;
        }
        public Student (String name, int id, int currYear, Faculty major, Faculty minor)
        {
            this.name = name;
            this.id = id;
            this.currYear = currYear;
            this.major = major;
            this.minor = minor;
        }

        public Boolean addCourseToCompleted(Course course)
        {
            //To prevent duplicates
            Boolean result = false;
            foreach (var cor in completedCourses)
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

        public List<Course> getCompletedCourses()
        {
            return completedCourses;
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

        public void removeCourseFromShortlist(Course course)
        {
            shortlist.Remove(course);
        }

        public List<Course> getShortlist()
        {
            return shortlist;
        }
    }
}
