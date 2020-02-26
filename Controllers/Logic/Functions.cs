using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Controllers.Logic
{
    public class Functions
    {
        private readonly DataContext _context;

        public Functions(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> registrationVerifierAsync(int sid, int cid)
        {
            List<Prerequisite> preRequisteList = await _context.Prerequisites.Where(p => p.courseId == cid).ToListAsync();
            foreach (Prerequisite require in preRequisteList)
            {
                if(_context.Enrolled.Where(e=> e.studentId==sid && e.courseId == cid && e.completed && e.grade>require.grade)==null)
                {
                    return false;
                }
            }
            return true;
        }

        //public async Task<bool> dropCourse(int sid, int cid)
        //{
        //    Enrolled row = _context.Enrolled.Where(e => e.studentId == sid && e.courseId == cid && !e.completed);
        //    if (eId != -1)
        //    {
        //        Enrolled row = _context.Enrolled.Find(eId);
        //        _context.Enrolled.Remove(row);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        ViewBag.message = "<scipt>alert('Failed Drop');</script>";
        //    }
        //}
    }
}
