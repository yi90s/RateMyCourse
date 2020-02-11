using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class CourseList
    {
        int id { get; }
        List<Course> courseList = new List<Course>();

        public CourseList(int id)
        {
            this.id = id;
        }

        public bool AddToCourseList(Course course)
        {
            //To prevent duplicates
            bool result = false;
            foreach (var cor in courseList) if (!result)
                {
                    if (cor.id == course.id)
                    {
                        courseList.Remove(cor);
                        result = true;
                    }
                }
            courseList.Add(course);
            return result;
        }

        public void RemoveFromCourseList(Course course)
        {
            courseList.Remove(course);
        }

        public List<Course> GetCoursesOffered()
        {
            return courseList;
        }
    }
}
