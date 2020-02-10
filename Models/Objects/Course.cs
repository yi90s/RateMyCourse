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
        public List<Course> preReqs;

        public Course(String name, int id, String desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
            preReqs = null;
        }
        public Course (String name, int id, int sectionid, String desc)
        {
            this.name = name;
            this.id = id;
            this.sectionid = sectionid;
            this.desc = desc;
            preReqs = null;
        }

        public Course(String name, int id, int sectionid, String desc, List<Course> preReqs)
        {
            this.name = name;
            this.id = id;
            this.sectionid = sectionid;
            this.desc = desc;
            this.preReqs = preReqs;
        }

        public Boolean addPreReq(Course course)
        {
            //To prevent duplicates
            Boolean result = false;
            if (preReqs != null)
            {
                foreach (var cor in preReqs)
                {
                    if (cor.id == course.id)
                    {
                        preReqs.Remove(cor);
                        result = true;
                        break;
                    }
                }
            } else //the list is null, and needs to be initialized
            {
                preReqs = new List<Course>();
            }
            preReqs.Add(course);
            return result;
        }

        public void removePreReq(Course course)
        {
            preReqs.Remove(course);
        }

        public List<Course> getPreReqs()
        {
            return preReqs;
        }
    }
}
