using cRegis.Core.Entities;
using cRegis.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace cRegis.Web.ViewModels
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

    public class CourseDetailViewModel
    {
        [DisplayName("Course Name")]
        public string courseName { get; set; }
        [DisplayName("Description")]
        public string courseDescription { get; set; }
        [DisplayName("Remaining Slots")]
        public int availableSpace { get; set; }
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [DisplayName("Rating Score")]
        public string avgRating { get; set; }
        public IEnumerable<CourseCommentViewModel> comments { get; set; }
    }

    public class CourseCommentViewModel
    {
        [DisplayName("Score")]
        public int ratingScore { get; set; }
        [DisplayName("Comment")]
        public string comment { get; set; }
        [DisplayName("Time Taken")]
        public DateTime takenDate { get; set; }
    }

    public class CourseContainerViewModel
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public ISet<CourseActions> actions { get; set; }
        public int? enrollId { get; set; }
        public int? studentId { get; set; }
    }

}
