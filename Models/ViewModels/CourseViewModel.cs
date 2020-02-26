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

        public int commentNum {get;set; }
        public Course thisCourse { get; set; }
        public List<CommentsViewModel> comments { get; set; }

        public CourseViewModel(int courseId,DataContext context)
        {
            comments = new List<CommentsViewModel>();
            enrollId = -1;
            this.thisCourse = context.Courses.Find(courseId);
            var sIdAndComments = context.Enrolled.Where(e => e.courseId == courseId && e.completed && e.comment!=null).ToDictionary(e => e.studentId, e => e.comment);
            int count = 0;
            foreach(KeyValuePair<int,string> sAndc in sIdAndComments)
            {
                int sid = sAndc.Key;
                Student stu = context.Students.Find(sid);
                CommentsViewModel newKeyPair = new CommentsViewModel(stu.name, sAndc.Value);
                comments.Add(newKeyPair);
                count++;
            }
            commentNum = count;
        }
    }
}
