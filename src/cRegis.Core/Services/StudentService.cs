using cRegis.Core.Data;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;

        public StudentService(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> getStudentAsync(int sid)
        {
            return await _context.Students.FindAsync(sid);
        }

        public int getRemainingCredithoursForStudent(Student student)
        {
            int creditHourNeed = _context.Faculties.Find(student.majorId).graduateCreditHours;
            int creditHourTook = 0;
            List<int> finshedCourseId =  _context.Enrolled.Where(e => e.studentId == student.studentId && e.completed).Select(e => e.courseId).ToList();
            foreach (int courseId in finshedCourseId)
            {
                creditHourTook += _context.Courses.Find(courseId).creditHours;
            }
            return creditHourNeed - creditHourTook;
        }

        public void registerCourseForStudent(Student student, Course course)
        {
            int cid = course.courseId;
            if (student != null)
            {
                Enrolled newEnroll = new Enrolled { courseId = cid, studentId = student.studentId, completed = false, grade = null, rating = null, comment = null };
                _context.Enrolled.Add(newEnroll);
                 _context.SaveChanges();
            }
        }

        public void updateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public bool verifyDropForStudent(Student student, int eid)
        {
            Enrolled thisEnroll = _context.Enrolled.Find(eid);

            bool result = (student.studentId == thisEnroll.studentId);

            return result;
        }

        public async Task<bool> verifyRegistrationForStudent(Student student, int cid)
        {
            //************************************************************************
            int totalSpace = _context.Courses.Find(cid).space;
            int occupied = _context.Enrolled.Where(e => e.courseId == cid && !e.completed).Count();
            int remainSpace = totalSpace - occupied;
            //************************************************************************

            if (student == null || remainSpace <= 0)
            {
                return false;
            }
            bool result = true;
            int sid = student.studentId;
            List<Prerequisite> prerequisiteList = await _context.Prerequisites.Where(p => p.courseId == cid).ToListAsync().ConfigureAwait(false);
            foreach (Prerequisite require in prerequisiteList)
            {
                List<Enrolled> thisEnrolls = await _context.Enrolled.Where(e => e.studentId == sid && e.courseId == cid && e.completed).ToListAsync();
                if (thisEnrolls != null)
                {
                    int grade = -1;
                    foreach (Enrolled enroll in thisEnrolls)
                    {
                        if (enroll.grade != null && enroll.grade > grade)
                        {
                            grade = enroll.grade.GetValueOrDefault();
                        }
                    }
                    if (grade < require.grade)
                    {
                        result = false;
                    }
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }


    }
}
