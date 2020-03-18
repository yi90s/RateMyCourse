using cRegis.Core.Entities;
using System.Collections.Generic;
namespace cRegis.Web.ViewModels
{
    public class HistoryViewModel
    {
        public Student thisStudent { get; set; }
        public IEnumerable<CourseContainerViewModel> courses { get; set; }

    }
}
