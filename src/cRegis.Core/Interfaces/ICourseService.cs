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

        List<Course> getCoursesInYear(int year);
        Task<List<Course>> getRecCoursesForStudentAsync(int sid);

        Task<List<Course>> getEligibleCoursesForStudentAsync(int sid);

        Task<List<Course>> getTakingCoursesForStudentAsync(int sid);

        List<Course> getCompletedCoursesForStudent(int sid);

        List<Comment> getCommentsForCourse(int cid);
    }
}
