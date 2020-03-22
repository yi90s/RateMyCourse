using cRegis.Core.Data;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Services
{
    public class WishlistService: IWishlistService
    {
        private readonly DataContext _context;

        public WishlistService(DataContext context)
        {
            _context = context;
        }

        public void addCoursetoStudentWishlist(int sid, int cid, int priority)
        {
            Wishlist newWishlistEntry = new Wishlist { studentId = sid, courseId = cid, priority = priority };
            _context.Wishlist.Add(newWishlistEntry);
            _context.SaveChanges();
        }
        public void removeCourseFromStudentWishlist(int sid, int cid)
        {
            Wishlist thisEntry = _context.Wishlist.Find(sid, cid);
            _context.Wishlist.Remove(thisEntry);
            _context.SaveChanges();
        }

        public List<Wishlist> getStudentWishlist(int sid)
        {
            return _context.Wishlist.Where(w => w.studentId == sid).ToList();
        }

        public bool isInWishlist(int sid, int cid)
        {
            return _context.Wishlist.Find(sid, cid) != null;
        }
    }
}
