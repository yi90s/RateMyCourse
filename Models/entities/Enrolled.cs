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
        public int EnrollId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public bool Completed { get; set; }

        public int Grade { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
