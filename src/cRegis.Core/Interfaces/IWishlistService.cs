using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace cRegis.Core.Interfaces
{
    public enum MoveDirection
    {
        MoveUp,
        MoveDown
    }

    public interface IWishlistService
    {
        void addCoursetoStudentWishlist(int sid, int cid);

        void removeCourseFromStudentWishlist(int sid, int cid);

        public IOrderedEnumerable<Wishlist> getStudentWishlist(int sid);

        public bool isInWishlist(int sid, int cid);

        public void movePriority(int sid, int cid, MoveDirection direction);
    }
}
