using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace cRegis.Core.Services
{
    public class EnrollService : IEnrollService
    {
        private readonly DataContext _context;
        public EnrollService(DataContext context)
        {
            _context = context;
        }

        public void drop(int eid)
        {
            Enrolled thisEnroll = _context.Enrolled.Find(eid);
            _context.Enrolled.Remove(thisEnroll);
            _context.SaveChanges();
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

        public void updateEnroll(Enrolled newEnroll)
        {
            if (newEnroll != null)
            {
                var change = _context.Enrolled.Update(newEnroll);
                if (change.State == EntityState.Modified)
                {
                    _context.SaveChanges();
                }
            }
        }
    }
}
