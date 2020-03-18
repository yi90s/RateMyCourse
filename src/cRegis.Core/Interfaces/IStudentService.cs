using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Interfaces
{
    public interface IStudentService
    {
        Task<Student> getStudentAsync(int sid);

        void updateStudent(Student student);

        void registerCourseForStudent(Student student, Course course);

        int getRemainingCredithoursForStudent(Student student);

        Task<bool> verifyRegistrationForStudent(Student student, int cid);

        bool verifyDropForStudent(Student student, int eid);


    }
}
