using cReg_WebApp.Models.context;
using cReg_WebApp.Models.DomainModels;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels.CourseViewModels
{
    public enum CourseActions
    {
        ViewDetail,
        RateCourse,
        DropCourse,
        RegisterCourse
    }

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
        public string avgRating { get; set; }
        public IEnumerable<CourseCommentViewModel> comments { get; set; }

        public CourseDetailViewModel(Course c, string rating, IEnumerable<CourseCommentViewModel> comments = null)
        {
            this.courseName = c.courseName;
            this.courseDescription = c.courseDescription;
            this.availableSpace = c.space;
            this.date = c.date;
            this.avgRating = rating;
            this.comments = comments;
        }
    }

    public class CourseCommentViewModel
    {

        [DisplayName("Score")]
        public int ratingScore { get; set; }
        [DisplayName("Comment")]
        public string comment { get; set; }
        [DisplayName("Time Taken")]
        public DateTime takenDate { get; set; }


        public CourseCommentViewModel(Comment comment)
        {
            this.ratingScore = comment.ratingScore ?? default(int);
            this.comment = comment.comment;
            this.takenDate = comment.takenDate;
        }
    }

    public class CourseContainerViewModel
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string courseDescription { get; set; }
        public ISet<CourseActions> actions { get; set; }

        public int? enrollId { get; set; }
        public int? studentId { get; set; }

        public CourseContainerViewModel(Course c, ISet<CourseActions> actions)
        {
            courseId = c.courseId;
            courseName = c.courseName;
            courseDescription = c.courseDescription;
            this.actions = actions;
        }

        public CourseContainerViewModel(Course c, ISet<CourseActions> actions, Enrolled e)
        {
            courseId = c.courseId;
            courseName = c.courseName;
            courseDescription = c.courseDescription;
            this.actions = actions;
            enrollId = e.enrollId;
        }

        public CourseContainerViewModel(Course c, ISet<CourseActions> actions, Student s)
        {
            courseId = c.courseId;
            courseName = c.courseName;
            courseDescription = c.courseDescription;
            this.actions = actions;
            studentId = s.studentId;
        }


    }

}
