using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class CourseViewModel
    {
        public int enrollId { get; set; }

        public string rate { get; set; }

        public int commentNum {get;set; }

        public int avaliableSpace { get; set; }

        public Course thisCourse { get; set; }

        public Dictionary<string, string> keyParis;


        public CourseViewModel(int enrollId,string rate,int commentNum, int avaliableSpace, Course thisCourse, Dictionary<string,string> keyValuePairs)
        {
            this.enrollId = enrollId;
            this.rate = rate;
            this.commentNum = commentNum;
            this.avaliableSpace = avaliableSpace;
            this.thisCourse = thisCourse;
            this.keyParis = keyValuePairs;
        }

        public CourseViewModel(string rate, int commentNum, int avaliableSpace, Course thisCourse, Dictionary<string, string> keyVPairs)
        {
            this.enrollId = -1;
            this.rate = rate;
            this.commentNum = commentNum;
            this.avaliableSpace = avaliableSpace;
            this.thisCourse = thisCourse;
            this.keyParis = keyVPairs;
        }

    }
}
