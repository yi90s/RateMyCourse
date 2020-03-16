using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.DomainModels
{
    public class Comment
    {
      
        public int? ratingScore { get; set; }
        public string comment { get; set; }
        public DateTime takenDate { get; set; }

        public Comment(Enrolled enroll)
        {
            this.ratingScore = enroll.rating;
            this.comment = enroll.comment;
            this.takenDate = enroll.course.date;
        }

    }
}
