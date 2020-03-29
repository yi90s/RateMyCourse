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

        void movePriority(int sid, int cid, MoveDirection direction);

        bool isInWishlist(int sid, int cid);

        IOrderedEnumerable<Wishlist> getStudentWishlist(int sid);
    }
}
