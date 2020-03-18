using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Entities
{
    public class Course
    {
        [Key]
        public int courseId { get; set; }
        
        public string courseName { get; set; }
        
        public string courseDescription { get; set; }
        public int creditHours { get; set; }
        public int space { get; set; }
        public DateTime date { get; set; }

    }
}
