using cRegis.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Mobile.Models.Entities
{
    public class Comment
    {
      
        public int ratingScore { get; set; }
        public string comment { get; set; }
        public DateTime takenDate { get; set; }


    }
}
