using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Student thisStudent;
        public string majorName;
        public Dictionary<int, Course> keyValues;

        public ProfileViewModel(int sid, DataContext context)
        {
            thisStudent = context.Students.Find(sid);
            if(thisStudent!=null)
            {
                keyValues = new Dictionary<int, Course>();
                Dictionary<int, int> temp = context.Enrolled.Where(e => e.studentId == sid && !e.completed).ToDictionary(e => e.enrollId, e => e.courseId);
                foreach (KeyValuePair<int, int> pair in temp)
                {
                    Course value = context.Courses.Find(pair.Value);
                    keyValues.Add(pair.Key, value);
                }
                majorName = context.Faculties.Find(thisStudent.majorId).facultyName;
            }
        }
    }
}
