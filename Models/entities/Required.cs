using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class Required
    {
        [Key]
        [Column(Order = 1)]
        public int FacultyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }

        public Faculty Faculty { get; set; }
        public Course Course { get; set; }
    }
}
