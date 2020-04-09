using cRegis.Core.Data;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Services
{
    public class EnrollService : IEnrollService
    {
        private readonly DataContext _context;
        public EnrollService(DataContext context)
        {
            _context = context;
        }
        public Enrolled drop(int eid)
        {
            Enrolled thisEnroll = _context.Enrolled.Find(eid);
            if (thisEnroll != null)
            {
                _context.Enrolled.Remove(thisEnroll);
                _context.SaveChanges();
            }
            return thisEnroll;
        }

        public List<Enrolled> getCompletedEnrollsForStudent(int sid)
        {
            List<Enrolled> completed = _context.Enrolled.Where(e => e.completed && e.studentId == sid).ToList();

            return completed;
        }

        public List<Enrolled> getCurrentEnrollsForStudent(int sid)
        {
            return _context.Enrolled.Where(e => e.studentId == sid && !e.completed).ToList();
        }

        public async Task<Enrolled> getEnrollAsync(int eid)
        {
            return await _context.Enrolled.FindAsync(eid);
        }

        public List<Enrolled> getEnrollsForStudent(int sid)
        {
            return _context.Enrolled.Where(e => e.studentId == sid).ToList();
        }

        public int updateEnroll(Enrolled newEnroll)
        {
            if (newEnroll == null)
            {
                return 1;
            }
            if (!_context.Enrolled.Contains(newEnroll))
            {
                return 2;
            }
            if (_context.Enrolled.Update(newEnroll).State != EntityState.Modified)
            {
                return 3;
            }
            _context.SaveChanges();
            return 0;
        }
    }
}
