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
        public Course thisCourse { get; set; }

        public Student user { get; set; }

        public Dictionary<string, string> keyParis;


        public CourseViewModel(int courseId,DataContext context)
        {
            enrollId = -1;
            this.thisCourse = context.Courses.Find(courseId);
            var sIdAndComments = context.Enrolled.Where(e => e.courseId == courseId && e.completed && e.comment!=null).ToDictionary(e => e.studentId, e => e.comment);
            int count = 0;
            int totalRate = 0;
            Array rating = context.Enrolled.Where(e => e.courseId == courseId && e.completed && e.rating != null).Select(e => e.rating).ToArray();
            keyParis = new Dictionary<string, string>();
            foreach (KeyValuePair<int,string> sAndc in sIdAndComments)
            {
                int sid = sAndc.Key;
                Student stu = context.Students.Find(sid);
                keyParis.Add(stu.name,sAndc.Value);
                count++;
            }
            commentNum = count;
            foreach (int singleRate in rating)
            {
                totalRate += singleRate;
            }
            if(count!=0)
            {
                rate = (totalRate / count).ToString("0")+"/100";
            }
            else
            {
                rate = "N/A";
            }
        }

        public void setEnrolled(int id, DataContext context)
        {
            enrollId = id;
            int userId = context.Enrolled.Find(id).studentId;
            user = context.Students.Find(userId);
        }
    }
}
