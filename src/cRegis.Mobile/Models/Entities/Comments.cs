using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.DTOs
{
    public class Comment
    {
      
        public int ratingScore { get; set; }
        public string comment { get; set; }
        public DateTime takenDate { get; set; }

        public Comment(Enrolled enroll)
        {
            this.ratingScore = enroll.rating ?? default(int);
            this.comment = enroll.comment;
            this.takenDate = enroll.course.date;
        }

    }
}
