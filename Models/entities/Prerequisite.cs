using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cReg_WebApp.Models.entities
{
    public class Prerequisite
    {
        [Key]
        [Column(Order = 1)]
        public int CourseId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PrerequisiteId { get; set; }
        public int Grade { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public Course PrerequisiteCourse { get; set; }

    }
}
