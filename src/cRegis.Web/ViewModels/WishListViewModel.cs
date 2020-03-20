using System.Collections.Generic;
using cRegis.Core.Entities;

namespace cRegis.Web.ViewModels
{
    public class WishListViewModel
    {
        public Student thisStudent { get; set; }
        public IEnumerable<CourseContainerViewModel> courses { get; set; }

    }
}
