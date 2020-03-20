using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cRegis.Web.Interfaces
{
    public enum CourseActions
    {
        ViewDetail,
        RateCourse,
        DropCourse,
        RegisterCourse,
        WishlistAdd,
        WishlistPriority,
        WishlistRemove
    }

    public interface IViewModelService
    {
        RateCourseViewModel buildRateCourseViewModel(Enrolled rate, Course course);

        CourseDetailViewModel buildCourseDetailViewModel(int cid);

        Task<ProfileViewModel> buildProfileViewModel(Student student);

        Task<FindCourseViewModel> buildFindCourseViewModel(Student student);

        HistoryViewModel buildHistoryViewModel(Student student);

        WishListViewModel buildWishListViewModel(Student student);

        CourseCommentViewModel buildCourseCommentViewModel(Comment comment);

        CourseContainerViewModel buildCourseContainerViewModel(Course course, ISet<CourseActions> actions, Enrolled enroll = null, Student s = null);
    }
}
