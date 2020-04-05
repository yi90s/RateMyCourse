using cRegis.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Mobile.Interfaces
{
    public interface IStudentService
    {
        Task<Student> getStudentAsync();
        Task<string> getStudentCreditAsync();
        Task<List<Course>> getStudentCourseListAsync();
        Task<List<Enrolled>> getStudentEnrolledListAsync();
        Task<Course> getCourseAsync(int cid);
        Task<Faculty> getFaculty(int fid);
    }
}
