using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class Completed
    {
        int studentId { get; set; }
        int courseId { get; set; }
        int grade { get; set; }
        string comment { get; set; }
        
       
        public Completed(int studentId, int courseId, int grade)
        {
            this.studentId = studentId;
            this.courseId = courseId;
            this.grade = grade;
            this.comment = "";
        }

    }
}
