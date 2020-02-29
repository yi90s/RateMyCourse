using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models
{
    public class StudentUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public string Password { get; set; }

        public Student student { get; set; }

    }
}
