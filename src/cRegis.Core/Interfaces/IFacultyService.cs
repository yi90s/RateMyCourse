using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace cRegis.Core.Interfaces
{
    public interface IFacultyService
    {
        Faculty getFaculty(int fid);
    }
}
