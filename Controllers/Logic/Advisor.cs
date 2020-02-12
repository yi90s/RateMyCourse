using cReg_WebApp.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers.Logic
{
    public class Advisor
    {
        public List<Course> GetRemainingPrerequisites(Student student, Course course)
        {
            List<Course> remainingCourses = new List<Course>();
            foreach (var cor in course.GetPreReqs())
            {
                if (student.GetCompletedCourses().Contains(cor) == false) //the student has not completed this (cor) prerequisite course yet
                {
                    remainingCourses.Add(cor);
                }
            }
            if (remainingCourses.Count > 0) //if there is at least 1 course remaining
            {
                return remainingCourses;
            }
            return null;
        }

        public List<Course> GetRecommendedCourses(Student student)
        {
            List<Course> recommendedCourses = new List<Course>();
            recommendedCourses.AddRange(GetCoursesInYear(student.major.GetCoursesOffered(), student.currYear));//returns courses in the major faculty of the same year in the program
            recommendedCourses.AddRange(GetCoursesInYear(student.minor.GetCoursesOffered(), -1));//returns courses in the minor faculty
            return recommendedCourses;
        }

        private List<Course> GetCoursesInYear(List<Course> courses, int year)
        {
            List<Course> result = new List<Course>();
            if (year == -1)
            {
                result.AddRange(courses);
            } else
            {
                foreach (var cor in courses)
                {
                    if (cor.id >= year * 1000 && cor.id < (year + 1) * 1000)
                    {
                        result.Add(cor);
                    }
                }
            }
            return result;
        }
    }
}
