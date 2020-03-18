using cRegis.Core.Entities;
using System.Collections.Generic;

namespace cRegis.Web.ViewModels
{
    public class ProfileViewModel
    {
        public Student thisStudent { get; set; }
        public string majorName { get; set; }
        public IEnumerable<CourseContainerViewModel> cViewModels { get; set; }
        public int remainingCreditHours { get; set; }

    }
}
