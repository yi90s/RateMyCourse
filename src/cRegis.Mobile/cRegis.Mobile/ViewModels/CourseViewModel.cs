using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;

namespace cRegis.Mobile.ViewModels
{
    class CourseViewModel
    {
        public List<Course> AllCourses { get; set; }

        /**public void test()
        {
            AllCourses = new List<Course>();
            AllCourses.Add(new Course() { courseId = 1, courseName = "COMP 4350", courseDescription = "Software Engineering 2", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 2, courseName = "COMP 4020", courseDescription = "HCI 2", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 3, courseName = "COMP 4490", courseDescription = "Graphic 2", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 4, courseName = "COMP 4190", courseDescription = "AI 2", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 5, courseName = "MATH 1500", courseDescription = "Caculus 1", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 6, courseName = "BIOL 1000", courseDescription = "BIOLOGY 1", creditHours = 3, space = 5, date = "N/A" });
            AllCourses.Add(new Course() { courseId = 7, courseName = "ASTR 1000", courseDescription = "Astronomy 1", creditHours = 3, space = 5, date = "N/A" });
        }**/

        public CourseViewModel(List<Course> l)
        {
            AllCourses = l;
        }
    }
}
