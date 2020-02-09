using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Course
    {
        String name;
        int id;
        String desc;

        public Course (String name, int id, String desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
        }
    }
}
