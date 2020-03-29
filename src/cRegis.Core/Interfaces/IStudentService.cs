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

        Task<int> registerCourseForStudent(int sid, int cid);

        int getRemainingCredithoursForStudent(int sid);

        Task<int> verifyRegistrationForStudent(int sid, int cid);

        bool verifyDropForStudent(int sid, int eid);

        Task<int> verifyDropForStudent(int sid, int eid);


    }
}
