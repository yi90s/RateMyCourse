using cRegis.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Identities
{
    public class StudentUser : IdentityUser
    {

        [Required]
        public int StudentId { get; set; }

        public Student Student { get; set; }

    }
}
