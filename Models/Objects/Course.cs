using System.Collections.Generic;

namespace cReg_WebApp.Models.Objects
{
    public class Course
    {
        public string name { get; }
        public int id { get; }
        public int sectionId { get; }
        public string desc { get; }
        public List<Course> preReqs = null;

        public Course(string name, int id, string desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
        }

        public Course(string name, int id, int sectionId, string desc) : this(name, id, desc)
        {
            this.sectionId = sectionId;
        }

        public Course(string name, int id, int sectionId, string desc, List<Course> preReqs) : this(name, id, sectionId, desc)
        {
            this.preReqs = preReqs;
        }

        public bool AddPreReq(Course course)
        {
            //To prevent duplicates
            bool result = false;
            if (preReqs != null)
            {
                foreach (var cor in preReqs) if (!result)
                {
                    if (cor.id == course.id)
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
