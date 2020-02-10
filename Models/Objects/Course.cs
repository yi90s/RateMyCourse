using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Course
    {
        private String name;
        private int id;
        private String desc;

        public Course (String name, int id, String desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
        }
    }
}
