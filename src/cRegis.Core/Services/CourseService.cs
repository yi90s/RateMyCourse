using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using cRegis.Core.DTOs;

namespace cRegis.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _context;

        public CourseService(DataContext context)
        {
            _context = context;
        }

        public int getAvailableSpaceForCourse(int cid)
        {
            int totalSpace = _context.Courses.Find(cid).space;
            int occupied =  _context.Enrolled.Where(e => e.courseId == cid && !e.completed).Count();
            return totalSpace-occupied;
        }

        public async Task<Course> getCourseAsync(int cid)
        {
            return await _context.Courses.FindAsync(cid);
        }

        public List<Course> getCoursesByKeywords(string keyword)
        {
            List<Course> result = _context.Courses.Where(c => c.courseName.Contains(keyword) || c.courseDescription.Contains(keyword)).ToList();

            return result;
        }

        public List<Course> getCoursesInYear(List<Course> courses, int year)
        {
            List<Course> result = new List<Course>();
            if (year == -1)
            {
                result.AddRange(courses);
            }
            else
            {
                foreach (var cor in courses)
                {
                    if (cor.courseId >= year * 1000 && cor.courseId < (year + 1) * 1000)
                    {
                        result.Add(cor);
                    }
                }
            }
            return result;
        }

        public async Task<List<Course>> getEligibleCoursesForStudentAsync(int sid)
        {
            List<Course> allCourses = await _context.Courses.ToListAsync();
            List<Course> takingCourses = await getTakingEnrollsForStudentAsync(sid);
            List<Course> takenCourses = getCompletedCoursesForStudent(sid);
            allCourses.RemoveAll(c => takingCourses.Contains(c) && takenCourses.Contains(c));

            return allCourses;
        }

        public async Task<List<Course>> getTakingEnrollsForStudentAsync(int sid)
        {
            List<Enrolled> enrolls = _context.Enrolled.Where(e => !e.completed && e.studentId == sid).ToList();
            List<Course> enrolledCourses = new List<Course>();
            enrolls.ForEach(
                e => enrolledCourses.Add(_context.Courses.Find(e.courseId))
                );

            return enrolledCourses;
        }

        public List<Course> getCompletedCoursesForStudent(int sid)
        {
            List<Enrolled> allRegs = _context.Enrolled.Where(e => e.studentId == sid).ToList();
            List<Enrolled> takens = allRegs.Where(e => e.completed).ToList();
            List<int> takenCourseIds = takens.Select(taken => taken.courseId).ToList();
            List<Course> takenCourses = _context.Courses.Where(c => takenCourseIds.Contains(c.courseId)).ToList();

            return takenCourses;
        }

        public async Task<List<Course>> getRecCoursesForStudentAsync(Student stu)
        {
            Dictionary<int, int?> finishedCourseIdAndGrade = await _context.Enrolled.Where(e => e.studentId == stu.studentId && e.completed).ToDictionaryAsync(e => e.courseId, e => e.grade);
            List<int> requiredCourseId = await _context.Required.Where(r => r.facultyId == stu.majorId).Select(r => r.courseId).ToListAsync();
            List<int> completedAndTakingCourseId = await _context.Enrolled.Where(e => e.studentId == stu.studentId).Select(e => e.courseId).ToListAsync();
            List<int> allCourseList = await _context.Courses.Where(c => !completedAndTakingCourseId.Contains(c.courseId)).Select(c => c.courseId).ToListAsync();
            List<Course> resultList = new List<Course>();
            List<Course> otherList = new List<Course>();

            //**************************************************************************
            int creditHourNeed = _context.Faculties.Find(stu.majorId).graduateCreditHours;
            int creditHourTook = 0;
            List<int> finshedCourseId = _context.Enrolled.Where(e => e.studentId == stu.studentId && e.completed).Select(e => e.courseId).ToList();
            foreach (int courseId in finshedCourseId)
            {
                creditHourTook += _context.Courses.Find(courseId).creditHours;
            }
            int rightNowCreditHours = creditHourNeed - creditHourTook;
            //**************************************************************************

            foreach (int courseId in allCourseList)
            {
                Dictionary<int, int> preRequisiteCourseIdAndGrade = await _context.Prerequisites.Where(p => p.courseId == courseId).ToDictionaryAsync(r => r.prerequisiteId, r => r.grade);
                bool completeAllPreRequisite = true;
                foreach (KeyValuePair<int, int> preIdAndGrade in preRequisiteCourseIdAndGrade)
                {
                    if (!finishedCourseIdAndGrade.ContainsKey(preIdAndGrade.Key))
                    {
                        completeAllPreRequisite = false;
                    }
                    else
                    {
                        int? grade = finishedCourseIdAndGrade[preIdAndGrade.Key];
                        if (grade < preIdAndGrade.Value)
                        {
                            completeAllPreRequisite = false;
                        }
                    }
                }
                if (completeAllPreRequisite && requiredCourseId.Contains(courseId))
                {
                    Course thisCourse = _context.Courses.Find(courseId);
                    rightNowCreditHours -= thisCourse.creditHours;
                    resultList.Add(thisCourse);
                }
                else if (completeAllPreRequisite)
                {
                    Course thisCourse = _context.Courses.Find(courseId);
                    otherList.Add(thisCourse);
                }
            }
            foreach (Course course in otherList)
            {
                if (rightNowCreditHours > 0)
                {
                    rightNowCreditHours -= course.creditHours;
                    resultList.Add(course);
                }
            }
            return resultList;
        }

        public Course getCourse(int cid)
        {
            return _context.Courses.Find(cid);
        }

        public List<Comment> getCommentsForCourse(int cid)
        {
            Course c = getCourse(cid);
            if (c == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            List<Enrolled> enrolls =  _context.Enrolled.Where(e => e.courseId == c.courseId && e.completed && e.comment!=null).ToList();
            foreach (var e in enrolls)
            {
                comments.Add(new Comment(e));
            }

            return comments;
        }
    }
}
