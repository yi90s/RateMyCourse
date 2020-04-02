using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Web.Interfaces;
using cRegis.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cRegis.Web.Services
{
    public class ViewModelService : IViewModelService
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollService _enrollService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _facultyService;

        public ViewModelService(ICourseService courseService, 
            IEnrollService enrollService, 
            IStudentService studentService,
            IFacultyService facultyService)
        {
            _facultyService = facultyService;
            _studentService = studentService;
            _courseService = courseService;
            _enrollService = enrollService;
        }

        public CourseCommentViewModel buildCourseCommentViewModel(Comment comment)
        {
            var vm = new CourseCommentViewModel
            {
                ratingScore = comment.ratingScore,
                comment = comment.comment,
                takenDate = comment.takenDate
            };

            return vm;
        }

        public CourseContainerViewModel buildCourseContainerViewModel(Course course, ISet<CourseActions> actions, Enrolled enroll = null, Student student = null)
        {
            var vm = new CourseContainerViewModel
            {
                courseId = course.courseId,
                courseName = course.courseName,
                courseDescription = course.courseDescription,
                actions = actions,
                enrollId = enroll?.enrollId,
                studentId = student?.studentId
            };

            return vm;
        }

        public CourseDetailViewModel buildCourseDetailViewModel(int cid)
        {
            Course c = _courseService.getCourse(cid);
            CourseDetailViewModel vm = null;
            List<CourseCommentViewModel> commentsVM = null;
            string avgRating = "N/A";

            if (c == null)
            {
                return null;
            }

            List<Comment> comments = _courseService.getCommentsForCourse(c.courseId);

            if (comments.Count > 0)
            { 
                commentsVM = new List<CourseCommentViewModel>();

                int ratingSum = 0;
                foreach (var cmt in comments)
                {
                    ratingSum += cmt.ratingScore;
                    commentsVM.Add(buildCourseCommentViewModel(cmt));
                }
                avgRating = (ratingSum / comments.Count).ToString() + "/100";

            }

            vm = new CourseDetailViewModel
            {
                courseName = c.courseName,
                courseDescription = c.courseDescription,
                availableSpace = _courseService.getAvailableSpaceForCourse(c.courseId),
                date = c.date,
                avgRating = avgRating,
                comments = commentsVM,
            };

            return vm;

        }

        public async Task<FindCourseViewModel> buildFindCourseViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };
            List<Course> eligibleCourses = await _courseService.getRecCoursesForStudentAsync(student.studentId);

            foreach (Course c in eligibleCourses)
            {
                var ccvm = buildCourseContainerViewModel(c, actions, student:student);
                ccvms.Add(ccvm);
            }

            FindCourseViewModel vmodel = new FindCourseViewModel { courseList = ccvms};

            return vmodel;
        }

        public  HistoryViewModel buildHistoryViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.RateCourse, CourseActions.ViewDetail };
            List<Enrolled> completed = _enrollService.getCompletedEnrollsForStudent(student.studentId);

            foreach (Enrolled enroll in completed)
            {
                Course course =  _courseService.getCourse(enroll.courseId);
                var ccvm = buildCourseContainerViewModel(course, actions, enroll:enroll);
                ccvms.Add(ccvm);
            }

            HistoryViewModel vmodel = new HistoryViewModel { thisStudent = student, courses = ccvms};

            return vmodel;
        }

        public async Task<ProfileViewModel> buildProfileViewModel(Student student)
        {
            if (student == null)
            {
                return null;
            }

            int remainCreditHours = _studentService.getRemainingCredithoursForStudent(student.studentId);
            string majorName =  _facultyService.getFaculty(student.majorId).facultyName;
            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.DropCourse };
            List<Enrolled> regCourses = _enrollService.getCurrentEnrollsForStudent(student.studentId);
            
            foreach (Enrolled e in regCourses)
            {
                Course thisCourse = _courseService.getCourse(e.courseId);
                var ccvm = buildCourseContainerViewModel(thisCourse, actions,e);
                ccvms.Add(ccvm);
            }

            ProfileViewModel vmodel = new ProfileViewModel {
                thisStudent = student,
                majorName = majorName,
                cViewModels = ccvms,
                remainingCreditHours = remainCreditHours
            };

            return vmodel;
        }

        public RateCourseViewModel buildRateCourseViewModel(Enrolled rate, Course course)
        {
            var vm = new RateCourseViewModel
            {
                EnrollId = rate.enrollId,
                Rating = rate.rating,
                Comment = rate.comment,
                CourseName = course.courseName,
                courseDescription = course.courseDescription
            };

            return vm;
        }
    }
}
