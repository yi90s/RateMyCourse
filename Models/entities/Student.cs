using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        public string password { get; set; }
        public int majorId { get; set; }
        public string name { get; set; }

        public ICollection<CompletedCourse> CompletedCourses { get; set; }
        
    }
}
