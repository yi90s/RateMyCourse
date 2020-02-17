using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class Enrolled
    {
        [Key]
        public int enrollId { get; set; }

        public int courseId { get; set; }

        public int studentId { get; set; }

        public Boolean completed { get; set; }

        public int grade { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }

        public Course course { get; set; }
        public Student student { get; set; }
    }
}
