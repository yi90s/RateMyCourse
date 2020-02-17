using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class CompletedCourse
    {
        [Key]
        public int studentId { get; set; }
        [Key]
        public int courseId { get; set; }
        public int grade { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }

        public Student student { get; set; }
        public Course course { get; set; }


    }
}
