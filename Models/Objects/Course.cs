using System.Collections.Generic;

namespace cReg_WebApp.Models.Objects
{
    public class Course
    {
        public string name { get; }
        public int id { get; }
        public int sectionId { get; }
        public string desc { get; }
        PrerequisiteList preReqList = null;

        public Course(string name, int id, string desc)
        {
            this.name = name;
            this.id = id;
            this.desc = desc;
            this.preReqList = new PrerequisiteList(id);
        }

        public Course(string name, int id, int sectionId, string desc) : this(name, id, desc)
        {
            this.sectionId = sectionId;
        }

        public Course(string name, int id, int sectionId, string desc, PrerequisiteList preReqs) : this(name, id, sectionId, desc)
        {
            this.preReqList = preReqs;
        }

        public bool AddPreReq(Course course)
        {
            return preReqList.AddPreReq(course);
        }

        public void RemovePreReq(Course course)
        {
            preReqList.RemovePreReq(course);
        }

        public List<Course> GetPreReqs()
        {
            return preReqList.GetPreReqs();
        }
    }
}
