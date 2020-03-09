using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.DomainModels
{
    public class Comment
    {
        [DisplayName("Score")]
        public string ratingScore { get; set; }
        [DisplayName("Comment")]
        public string comment { get; set; }
        [DisplayName("Time Rated")]
        public DateTime date { get; set; }

    }
}
