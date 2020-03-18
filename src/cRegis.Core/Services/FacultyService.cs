using cRegis.Core.Data;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;

namespace cRegis.Core.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly DataContext _context;

        public FacultyService(DataContext context)
        {
            _context = context;
        }

        public Faculty getFaculty(int fid)
        {
            return _context.Faculties.Find(fid);
        }
    }
}
