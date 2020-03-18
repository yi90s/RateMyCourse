using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Interfaces
{
    public interface ICourseService
    {
        Course getCourse(int cid);
        Task<Course> getCourseAsync(int cid);

        List<Course> getCoursesByKeywords(string keyword);

        int getAvailableSpaceForCourse(int cid);

        List<Course> getCoursesInYear(List<Course> courses, int year);
        Task<List<Course>> getRecCoursesForStudentAsync(Student student);

        Task<List<Course>> getEligibleCoursesForStudentAsync(Student stu);

        Task<List<Course>> getTakingCoursesForStudentAsync(Student stu);

        List<Course> getCompletedCoursesForStudent(Student stu);
        List<Comment> getCommentsForCourse(int cid);
    }
}
