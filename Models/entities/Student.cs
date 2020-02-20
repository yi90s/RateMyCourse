using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cReg_WebApp.Models.entities
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentId { get; set; }
        public string Password { get; set; }
        public int MajorId { get; set; }
        public string Name { get; set; }

        public ICollection<Enrolled> CompletedCourses { get; set; }

    }
}
