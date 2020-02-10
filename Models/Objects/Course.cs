using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Course
    {
        public String name { get; }
        public int id { get; }
        public int sectionid { get; }
        public String desc { get; }

        public Course(String name, int id, String desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
        }
        public Course (String name, int id, int sectionid, String desc)
        {
            this.name = name;
            this.id = id;
            this.sectionid = sectionid;
            this.desc = desc;
        }
    }
}
