using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class RateCourseViewModel : ViewModelBase
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
        public string CourseFullName { get; set; }
        public string Instructor { get; set; }
        public string date { get; set; }
        

        public RateCourseViewModel()
        {

        }
        public RateCourseViewModel(Enrolled enroll, Course courseDetail)
        {
            this.EnrollId = enroll.enrollId;
            this.Rating = enroll.rating;
            this.Comment = enroll.comment;
            this.CourseFullName= courseDetail.courseName + " - " + courseDetail.courseDescription;
        }
    }
}
