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

        public void addCoursetoStudentWishlist(int sid, int cid)
        {
            int lastPriorityNum = 0;
            if (_context.Wishlist.Any(w => w.studentId == sid))
            {
                lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
            }
            Wishlist newWishlistEntry = new Wishlist {studentId = sid, courseId = cid, priority = lastPriorityNum+1};
            _context.Wishlist.Add(newWishlistEntry);
            _context.SaveChanges();
        }
        public void removeCourseFromStudentWishlist(int sid, int cid)
        {
            Wishlist thisEntry = _context.Wishlist.Find(sid, cid);
            _context.Wishlist.Remove(thisEntry);

            int lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);
            if(thisEntry.priority < lastPriorityNum)
            {
                IOrderedEnumerable<Wishlist> entriesToModify = _context.Wishlist.Where(w => w.studentId == sid && w.priority > thisEntry.priority).ToList().OrderBy(w => w.priority);
                foreach(Wishlist entry in entriesToModify)
                {
                    entry.priority = entry.priority - 1;
                    _context.Wishlist.Update(entry);
                }
            }
            _context.SaveChanges();
        }

        public void movePriority(int sid, int cid, MoveDirection direction)
        {
            Wishlist sourceEntry = _context.Wishlist.Find(sid, cid);
            int sourceEntryPriority = sourceEntry.priority;

            int lastPriorityNum = _context.Wishlist.Where(w => w.studentId == sid).Max(w => w.priority);

            int destinationEntryPriority = -1;
            if (direction == MoveDirection.MoveUp && sourceEntryPriority > 1)
            {
                destinationEntryPriority = sourceEntryPriority - 1;
            }
            else if (direction == MoveDirection.MoveDown && sourceEntryPriority < lastPriorityNum)
            {
                destinationEntryPriority = sourceEntryPriority + 1;
            }
            else
            {
                return;
            }

            Wishlist destinationEntry = _context.Wishlist.FirstOrDefault(w => w.studentId == sid && w.priority == destinationEntryPriority);

            sourceEntry.priority = destinationEntryPriority;
            destinationEntry.priority = sourceEntryPriority;

            var change1 = _context.Wishlist.Update(sourceEntry);
            var change2 = _context.Wishlist.Update(destinationEntry);
            if (change1.State == EntityState.Modified && change2.State == EntityState.Modified)
            {
                _context.SaveChanges();
            }
        }

        public bool isInWishlist(int sid, int cid)
        {
            return _context.Wishlist.Find(sid, cid) != null;
        }

        public IOrderedEnumerable<Wishlist> getStudentWishlist(int sid)
        {
            List<Wishlist> wishlist = _context.Wishlist.Where(w => w.studentId == sid).ToList();
            IOrderedEnumerable<Wishlist> orderedWishlist = wishlist.OrderBy(w => w.priority);
            return orderedWishlist;
        }

    }
}
