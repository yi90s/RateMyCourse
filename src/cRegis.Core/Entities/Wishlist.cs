using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace cRegis.Core.Entities
{
    public class Wishlist
    {
        [Key]
        [Column(Order = 1)]
        public int studentId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int courseId { get; set; }

        public Student student { get; set; }
        public Course course { get; set; }

        public int priority { get; set; }

    }
}
