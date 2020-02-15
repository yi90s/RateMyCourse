using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cReg_WebApp.Models.Objects
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; }
        public Faculty Major { get; set; }
        public Faculty Minor { get; set; }
        public int CurrYear { get; set; }
        List<Course> Shortlist => new List<Course>();
        List<Course> CompletedCourses => new List<Course>();

        public Student() { }

        public Student (string name, int id, int currYear)
        {
            Name = name;
            Id = id;
            CurrYear = currYear;
        }
        public Student (string name, int id, int currYear, Faculty major, Faculty minor)
        {
            Name = name;
            Id = id;
            CurrYear = currYear;
            Major = major;
            Minor = minor;
        }

        public bool AddCourseToCompleted(Course course)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var cor in CompletedCourses) if (!result)
            {
                if (cor.Id == course.Id)
                {
                    Shortlist.Remove(cor);
                    result = true;
                }
            }
            Shortlist.Add(course);
            return result;
        }

        public List<Course> GetCompletedCourses()
        {
            return CompletedCourses;
        }

        public bool AddCourseToShortlist(Course course)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var cor in Shortlist) if (!result)
            {
                if (cor.Id == course.Id)
                {
                    Shortlist.Remove(cor);
                    result = true;
                }
            }
            Shortlist.Add(course);
            return result;
        }

        public void RemoveCourseFromShortlist(Course course)
        {
            Shortlist.Remove(course);
        }

        public List<Course> GetShortlist()
        {
            return Shortlist;
        }
    }
}
