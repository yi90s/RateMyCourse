using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models
{
    public class StudentUser : IdentityUser
    {

        [Required]
        public int StudentId { get; set; }

        public Student Student { get; set; }

    }
}
