using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers
{
    public class Faculty
    {
        String name;
        String id;
        HashSet<Course> courseSet = new HashSet<Course>();
    }
}