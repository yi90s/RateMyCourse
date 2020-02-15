using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cReg_WebApp.Models.Objects
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SectionId { get; set; }
        public string Description { get; set; }
        public PrerequisiteList PreReqList = null;

        public Course() { }

        public Course(string name, string desc, string sectionId)
        {
            Name = name;
            Description = desc;
            SectionId = sectionId;
        }

        public Course(string name, int id, string desc)
        {
            Name = name;
            Id = id;
            Description = desc;
        }

        public Course(string name, int id, string sectionId, string desc) : this(name, id, desc)
        {
            SectionId = sectionId;
        }
        
        public Course(string name, int id, string sectionId, string desc, PrerequisiteList preReqs) : this(name, id, sectionId, desc)
        {
            PreReqList = preReqs;
        }
        public bool AddPreReq(Course course)
        {
            return PreReqList.AddPreReq(course);
        }

        public void RemovePreReq(Course course)
        {
            PreReqList.RemovePreReq(course);
        }

        public List<Course> GetPreReqs()
        {
            return PreReqList.GetPreReqs();
        }
    }
}
