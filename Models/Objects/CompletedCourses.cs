using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    //TODO other methods
    public class CompletedCourses
    {
        int id { get; }
        List<Course> courses = null;

        public CompletedCourses(int id)
        {
            this.id = id;
        }

        public Boolean AddCourseToCompleted(Course course)
        {
            bool result = false;
            foreach (var cor in courses) if (!result)
                {
                    if (cor.Id == course.Id)
                    {
                        courses.Remove(cor);
                        result = true;
                    }
                }
            courses.Add(course);
            return result;
        }

        public List<Course> GetCompletedCourses()
        {
            return courses;
        }
    }
}
