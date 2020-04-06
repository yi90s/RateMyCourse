using System;
using System.Collections.Generic;
using System.Text;
using cRegis.Mobile.Models.Entities;

namespace cRegis.Mobile.ViewModels
{
    class HistoryViewModel
    {
        public List<EnrolledViewModel> AllCourses { get; set; }

        public HistoryViewModel(List<EnrolledViewModel> l)
        {
            AllCourses = l;
        }
    }
}
