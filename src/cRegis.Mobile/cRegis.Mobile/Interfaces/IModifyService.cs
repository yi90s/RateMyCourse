using cRegis.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Mobile.Interfaces
{
    public interface IModifyService
    {
        Task<string> registerCourseAsync(int cid);
        Task<string> dropCourseAsync(int eid);
    }
}
