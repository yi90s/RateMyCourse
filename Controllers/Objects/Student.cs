using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Student
    {
        String name;
        int id;
        Faculty major { get; set; }
        Faculty minor { get; set; }

        public Student (String name, int id)
        {
            this.name = name;
            this.id = id;
        }
        public Student (String name, int id, Faculty major, Faculty minor)
        {
            this.name = name;
            this.id = id;
            this.major = major;
            this.minor = minor;
        }
    }
}
