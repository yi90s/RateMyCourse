using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.ViewModels
{
    class StudentViewModel
    {

        public string studentName { get; set; }
        public int studentID { get; set; }

        public string facultyName { get; set; }

        public string creditRemain { get; set; }

        public List<EnrolledViewModel> enrolledlist { get; set; }

        public StudentViewModel(Student s, string crehrs, List<EnrolledViewModel> listC)
        {
            studentName = s.name;
            studentID = s.studentId;
            facultyName = "Computer Science";
            creditRemain = crehrs;
            //creditRemain = "120";
            enrolledlist = listC;
        }

    }
}
