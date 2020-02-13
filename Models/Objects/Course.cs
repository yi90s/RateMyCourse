using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cReg_WebApp.Controllers
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; }
        public int SectionId { get; }
        public string Desc { get; }
        public List<Course> PreReqs = null;

        public Course() { }

        public Course(string name, int id, string desc)
        {
            Name = name;
            Id = id;
            Desc = desc;
        }

        public Course(string name, int id, int sectionId, string desc) : this(name, id, desc)
        {
            SectionId = sectionId;
        }

        public Course(string name, int id, int sectionId, string desc, List<Course> preReqs) : this(name, id, sectionId, desc)
        {
            PreReqs = preReqs;
        }

        public bool AddPreReq(Course course)
        {
            //To prevent duplicates
            bool result = false;
            if (PreReqs != null)
            {
                foreach (var cor in PreReqs) if (!result)
                {
                    if (cor.Id == course.Id)
                    {
                        PreReqs.Remove(cor);
                        result = true;
                    }
                }
            }
            else //the list is null, and needs to be initialized
            {
                PreReqs = new List<Course>();
            }
            PreReqs.Add(course);
            return result;
        }

        public void RemovePreReq(Course course)
        {
            PreReqs.Remove(course);
        }

        public List<Course> GetPreReqs()
        {
            return PreReqs;
        }
    }
}
