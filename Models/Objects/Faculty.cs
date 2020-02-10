using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Faculty
    {
        private String name;
        private int id;
        private HashSet<Course> courseSet = new HashSet<Course>();

        public Faculty(String name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public void addToCourseSet(Course course)
        {
            courseSet.Add(course);
        }

        public void removeFromCourseSet(Course course)
        {
            courseSet.Remove(course);
        }
    }
}