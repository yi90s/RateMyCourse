using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class CompletedCourses
    {
        int id;
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
                    if (cor.id == course.id)
                    {
                        courses.Remove(cor);
                        result = true;
                    }
                }
            courses.Add(course);
            return result;
        }
    }
}
