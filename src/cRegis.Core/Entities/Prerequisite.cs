using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Entities
{
    public class Prerequisite
    {
        [Key]
        [Column(Order = 1)]
        public int courseId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int prerequisiteId { get; set; }
        public int grade { get; set; }

        public Course course { get; set; }
        public Course prerequisite { get; set; }

    }
}
