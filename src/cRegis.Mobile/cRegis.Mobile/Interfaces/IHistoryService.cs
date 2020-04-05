using cRegis.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Mobile.Interfaces
{
    public interface IHistoryService
    {
        Task<List<Enrolled>> getHistoryEnrolledListAsync();
        Task<string> postCommentAsync(Enrolled e);
    }
}
