using cRegis.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace cRegis.Mobile.Models.Entities
{
    public class Student
    {
        //[Key]
        public int studentId { get; set; }
        public string password { get; set; }
        public int majorId { get; set; }
        public string name { get; set; }

        public Faculty major { get; set; }
        public ICollection<Enrolled> enrolledCourses { get; set; }

    }
}
