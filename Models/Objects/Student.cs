using System.Collections.Generic;

namespace cReg_WebApp.Models.Objects
{
    public class Student
    {
        public string name { get; }
        public int id { get; }
        public Faculty major { get; set; }
        public Faculty minor { get; set; }
        public int currYear { get; set; }
        Shortlist shortlist = null;
        List<Course> CompletedCourses => new List<Course>();

        public Student (string name, int id, int currYear)
        {
            this.name = name;
            this.id = id;
            this.currYear = currYear;
            this.shortlist = new Shortlist(id);
        }
        public Student (string name, int id, int currYear, Faculty major, Faculty minor) : this(name, id, currYear)
        {
            this.major = major;
            this.minor = minor;
        }

        public bool AddCourseToCompleted(Course course)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var cor in CompletedCourses) if (!result)
            {
                if (cor.id == course.id)
                {
                        CompletedCourses.Remove(cor);
                    result = true;
                }
            }
            CompletedCourses.Add(course);
            return result;
        }

        public List<Course> GetCompletedCourses()
        {
            return CompletedCourses;
        }

        public bool AddCourseToShortlist(Course course)
        {
            return shortlist.AddCourseToShortlist(course);
        }

        public void RemoveCourseFromShortlist(Course course)
        {
            shortlist.RemoveCourseFromShortlist(course);
        }

        public List<Course> GetShortlist()
        {
            return shortlist.getShortlist();
        }
    }
}
