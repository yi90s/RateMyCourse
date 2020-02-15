using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class Shortlist
    {
        int id;
        List<Course> list = null;

        public Shortlist(int id)
        {
            this.id = id;
            list = new List<Course>();
        }

        public Shortlist(int id, List<Course> courses) : this(id)
        {
            list = courses;
        }

        public Boolean AddCourseToShortlist(Course course)
        {
            bool result = false;
            foreach (var cor in list) if (!result)
                {
                    if (cor.Id == course.Id)
                    {
                        list.Remove(cor);
                        result = true;
                    }
                }
            list.Add(course);
            return result;
        }

        public void RemoveCourseFromShortlist(Course course)
        {
            list.Remove(course);
        }

        public List<Course> getShortlist()
        {
            return list;
        }
    }
}
