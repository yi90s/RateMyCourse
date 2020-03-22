using cRegis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Core.Interfaces
{
    public interface IWishlistService
    {
        void addCoursetoStudentWishlist(int sid, int cid, int priority);

        void removeCourseFromStudentWishlist(int sid, int cid);

        List<Wishlist> getStudentWishlist(int sid);

        public bool isInWishlist(int sid, int cid);
    }

}
