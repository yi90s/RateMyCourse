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

        public int getRemainingCredithoursForStudent(int sid)
        {
            Student student = _context.Students.Find(sid);
            if (student == null)
            {
                return -1;
            }

            int creditHourNeed = _context.Faculties.Find(student.majorId).graduateCreditHours;
            int creditHourTook = 0;
            List<int> finshedCourseId = _context.Enrolled.Where(e => e.studentId == student.studentId && e.completed).Select(e => e.courseId).ToList();
            foreach (int courseId in finshedCourseId)
            {
                creditHourTook += _context.Courses.Find(courseId).creditHours;
            }
            return creditHourNeed - creditHourTook;
        }

        public async Task<int> registerCourseForStudent(int sid, int cid)
        {
            if (await _context.Students.FindAsync(sid) == null) {
                return 1;
            }
            if (await _context.Courses.FindAsync(cid) == null)
            {
                return 2;
            }
            Enrolled newEnroll = new Enrolled { courseId = cid, studentId = sid, completed = false, grade = null, rating = null, comment = null };
            _context.Enrolled.Add(newEnroll);
            _context.SaveChanges();
            return 0;
        }

        public async Task<int> verifyDropForStudent(int sid, int eid)
        {
            if (await _context.Enrolled.FindAsync(eid) == null)
            {
                return 1;
            }
            if (sid == _context.Enrolled.Find(eid).studentId)
            {
                return 0;
            }
            return -1;
        }

        public async Task<int> verifyRegistrationForStudent(int sid, int cid)
        {
            if (await _context.Students.FindAsync(sid) == null)
            {
                return 1;
            }
            if (await _context.Courses.FindAsync(cid) == null)
            {
                return 2;
            }

            Student student = _context.Students.Find(sid);

            //************************************************************************
            int totalSpace = _context.Courses.Find(cid).space;
            int occupied = _context.Enrolled.Where(e => e.courseId == cid && !e.completed).Count();
            int remainSpace = totalSpace - occupied;
            //************************************************************************

            if (student == null || remainSpace <= 0)
            {
                return -1;
            }
            int result = 0;
            List<Prerequisite> prerequisiteList = await _context.Prerequisites.Where(p => p.courseId == cid).ToListAsync().ConfigureAwait(false);
            foreach (Prerequisite require in prerequisiteList)
            {
                List<Enrolled> thisEnrolls = await _context.Enrolled.Where(e => e.studentId == sid && e.courseId == require.prerequisiteId && e.completed).ToListAsync();
                if (thisEnrolls.Count > 0)
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
                        result = -1;
                    }
                }
                else
                {
                    result = -1;
                }
            }
            return result;
        }
    }
}
