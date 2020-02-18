using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class Course
    {
        [Key]
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public int creditHours { get; set; }
        public int space { get; set; }
        public String date { get; set; }

    }
}
