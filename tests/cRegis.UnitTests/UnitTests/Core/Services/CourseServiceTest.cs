using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{
    public class CourseServiceTest : TestBase
    {
        private const int ELIGIBLE_COURSE_COUNT = 26;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        public CourseServiceTest()
        {
            _courseService = new CourseService(_context);
            _studentService = new StudentService(_context);
        }

        [Fact]
        public void FindCourseWithNullId()
        {
            Course c = _courseService.getCourse(-1);
            Assert.Null(c);
        }

        [Theory]
        [InlineData(1, "An Introduction to Computer Science 1", "COMP 1010")]
        [InlineData(2, "An Introduction to Computer Science 2", "COMP 1020")]
        [InlineData(3, "Data Structure and Algorithm", "COMP 2140")]
        [InlineData(4, "Program Practice", "COMP 2160")]
        [InlineData(5, "Analysis of Algorithms", "COMP 2080")]
        public void FindCourseWithId(int courseId, string courseDesc, string courseName)
        {
            Course crs = _courseService.getCourse(courseId);
            Assert.NotNull(crs);
            Assert.Equal(courseId, crs.courseId);
            Assert.Equal(courseName, crs.courseName);
            Assert.Equal(courseDesc, crs.courseDescription);
        }

        [Theory]
        [InlineData(1, "An Introduction to Computer Science 1", "COMP 1010")]
        [InlineData(2, "An Introduction to Computer Science 2", "COMP 1020")]
        [InlineData(3, "Data Structure and Algorithm", "COMP 2140")]
        [InlineData(4, "Program Practice", "COMP 2160")]
        [InlineData(5, "Analysis of Algorithms", "COMP 2080")]
        public async Task FindCourseWithIdAsync(int courseId, string courseDesc, string courseName)
        {
            Course crs = await _courseService.getCourseAsync(courseId);
            Assert.NotNull(crs);
            Assert.Equal(courseId, crs.courseId);
            Assert.Equal(courseName, crs.courseName);
            Assert.Equal(courseDesc, crs.courseDescription);
        }

        [Theory]
        [InlineData("Database Implementation", "COMP 4380")]
        [InlineData("Operating Systems", "COMP 3430")]
        [InlineData("Object Orientation", "COMP 2150")]
        [InlineData("Computer Science 2", "COMP 1020")]
        public void FindCourseByKeyword(string keyword, string courseName)
        {
            var courseList = _courseService.getCoursesByKeywords(keyword);
            Assert.NotEmpty(courseList);
            Assert.Single(courseList);
            Assert.Equal(courseName, courseList[0].courseName);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 5)]
        [InlineData(3, 5)]
        [InlineData(4, 5)]
        [InlineData(5, 5)]
        public void GetAvailableSpaceForCourse(int courseId, int space)
        {
            var courseSpace = _courseService.getAvailableSpaceForCourse(courseId);
            Assert.Equal(space, courseSpace);
            // if the amount of space for one of these courses changes, this will fail. I don't know if we want to expect that to happen or not
        }

        [Theory]
        [InlineData(1, 8)]
        [InlineData(2, 6)]
        [InlineData(3, 7)]
        [InlineData(4, 5)]
        public void GetCoursesInYear(int year, int coursesThisYear)
        {
            var courses = _courseService.getCoursesInYear(year);
            Assert.NotEmpty(courses);
            var numCourses = courses.Count;
            Assert.Equal(coursesThisYear, numCourses);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetRecCoursesForStudentAsync(int studentId)
        {
            var student = await _studentService.getStudentAsync(studentId);
            var courseList = await _courseService.getRecCoursesForStudentAsync(student);
            Assert.NotEmpty(courseList);
            // we could check to see if the number of recommended courses for each student matches a certain amount
            // or if a certain course exists in the recommendations but those might yet change
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetEligibleCoursesForStudentAsync(int studentId)
        {
            var courseList = await _courseService.getEligibleCoursesForStudentAsync(studentId);
            Assert.NotEmpty(courseList);
            var countEligibleCourses = courseList.Count;
            Assert.Equal(ELIGIBLE_COURSE_COUNT, countEligibleCourses);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        public async Task GetEnrolledCourseByStudentIdAsync(int studentId, int numEnrolledCourses)
        {
            var courseList = await _courseService.getTakingEnrollsForStudentAsync(studentId);
            var enrolledCourseCount = courseList.Count;
            Assert.Equal(numEnrolledCourses, enrolledCourseCount);
            // only issue here is if the number of enrolled courses changes then this test will fail... What else can we test here?
        }

        [Theory]
        [InlineData(1, 12)]
        [InlineData(2, 10)]
        [InlineData(3, 4)]
        public void GetCompletedCoursesForStudent(int studentId, int numCompleted)
        {
            var courseList = _courseService.getCompletedCoursesForStudent(studentId);
            Assert.NotEmpty(courseList);
            var completedCount = courseList.Count;
            Assert.True(numCompleted <= completedCount);
            // I chose to see if the completed courses was greater than the given number in case they complete further courses after this - this may not be necessary
        }

        [Theory]
        [InlineData(1, 7, "I like that course", 90)]
        [InlineData(2, 7, "Very good", 80)]
        [InlineData(3, 2, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand", 75)]
        public void GetCommentsForCourse(int courseId, int commentCountExpected, string expectedComment, int expectedRating)
        {
            var commentList = _courseService.getCommentsForCourse(courseId);
            Assert.NotEmpty(commentList);
            var numCommentsFound = commentList.Count;
            Assert.Equal(commentCountExpected, numCommentsFound);
            Assert.True(commentList.Exists(c => c.comment.Equals(expectedComment)));
            Assert.True(commentList.Exists(c => c.ratingScore.Equals(expectedRating)));
        }
    }
}
