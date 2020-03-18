using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace cRegis.Web.ViewModels
{
    public class RateCourseViewModel
    {
        
        public int EnrollId { get; set; }
        [DisplayName("Your Rating(0-100)")]
        [Range(0,100)]
        [Required]
        public int? Rating { get; set; }
        [DisplayName("Your Comment")]
        [StringLength(100)]
        [Required]
        public string Comment { get; set; }
        public string CourseName { get; set; }
        public string courseDescription { get; set; }
        public string Instructor { get; set; }
        public string date { get; set; }
       
    }
}
