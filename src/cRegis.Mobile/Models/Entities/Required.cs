using System;
using System.Collections.Generic;
using System.Text;

namespace cReg_Mobile.Models.Entities
{
    public class Required
    {
        //[Key]
        //[Column(Order = 1)]
        public int facultyId { get; set; }
        //[Key]
        //[Column(Order = 2)]
        public int courseId { get; set; }

        public Faculty faculty { get; set; }
        public Course course { get; set; }
    }
}
