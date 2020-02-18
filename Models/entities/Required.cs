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
        public int facultyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int courseId { get; set; }

        public Faculty faculty { get; set; }
        public Course course { get; set; }
    }
}
