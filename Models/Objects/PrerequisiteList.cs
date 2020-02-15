using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models.Objects
{
    public class PrerequisiteList
    {
        string id { get; }
        List<Course> preReqs = new List<Course>();

        public PrerequisiteList(string id)
        {
            this.id = id;
        }

        public bool AddPreReq(Course course)
        {
            //To prevent duplicates
            bool result = false;
            if (preReqs != null)
            {
                foreach (var cor in preReqs) if (!result)
                    {
                        if (cor.Id == course.Id)
                        {
                            preReqs.Remove(cor);
                            result = true;
                        }
                    }
            }
            else //the list is null, and needs to be initialized
            {
                preReqs = new List<Course>();
            }
            preReqs.Add(course);
            return result;
        }

        public void RemovePreReq(Course course)
        {
            preReqs.Remove(course);
        }

        public List<Course> GetPreReqs()
        {
            return preReqs;
        }
    }
}
