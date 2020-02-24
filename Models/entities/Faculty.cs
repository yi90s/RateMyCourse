using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.entities
{
    public class Faculty
    {
        [Key]
        public int facultyId { get; set; }
        public string facultyName { get; set; }

    }
}
