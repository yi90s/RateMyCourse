using System;
using System.Collections.Generic;
using System.Text;

namespace cReg_Mobile.Models.Entities
{
    public class Prerequisite
    {
        //[Key]
        //[Column(Order = 1)]
        public int courseId { get; set; }
        //[Key]
        //[Column(Order = 1)]
        public int prerequisiteId { get; set; }
        public int grade { get; set; }

        public Course course { get; set; }
        public Course prerequisite { get; set; }

    }
}
