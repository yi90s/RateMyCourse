using cReg_WebApp.Models.context;
using cReg_WebApp.Models.DomainModels;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public entities.Course thisCourse { get; set; }

        public Dictionary<string, string> keyParis;


        public CourseViewModel(int enrollId,string rate,int commentNum, int avaliableSpace, entities.Course thisCourse, Dictionary<string,string> keyValuePairs)
        {
            this.enrollId = enrollId;
            this.rate = rate;
            this.commentNum = commentNum;
            this.avaliableSpace = avaliableSpace;
            this.thisCourse = thisCourse;
            this.keyParis = keyValuePairs;
        }

        public CourseViewModel(string rate, int commentNum, int avaliableSpace, entities.Course thisCourse, Dictionary<string, string> keyVPairs)
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
        public string ratingScore { get; set; }

        public CourseDetailViewModel(Course c, string rating)
        {
            this.courseName = c.courseName;
            this.courseDescription = c.courseDescription;
            this.availableSpace = c.space;
            this.date = c.date;
            this.ratingScore = rating;
        }
    }

    public class CourseCommentsViewModel
    {
        public List<Comment> relatedComments { get; set; }

        public CourseCommentsViewModel(List<Comment> comments)
        {
            this.relatedComments = comments;
        }
    }
}
