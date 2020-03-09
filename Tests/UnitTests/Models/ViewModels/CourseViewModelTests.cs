//using cReg_WebApp.Models.ViewModels;
//using cReg_WebApp.test.Infrastructure;
//using System.Linq;
//using Xunit;

//namespace cReg_WebApp.Tests.UnitTests.Models.ViewModels
//{
//    public class CourseViewModelTests : TestBase
//    {

//        [Fact]
//        public void NullCourseForConstructor()
//        {
//            Arrange
//           var result = new CourseViewModel(5, _context);

//            Assert.True(result.thisCourse == null, "Course should be null");

//            result = new CourseViewModel(-1, _context);

//            Assert.True(result.thisCourse == null, "Course should be null");
//        }


//        [Fact]
//        public void CourseObjectForConstructor()
//        {
//            var result = new CourseViewModel(1, _context);

//            Assert.Equal("COMP 4380", result.thisCourse.courseName);
//            Assert.Equal("Database Implementation", result.thisCourse.courseDescription);
//            Assert.True(result.thisCourse.creditHours == 3, "Credit hour shold be 3");
//            Assert.True(result.thisCourse.space == 80, "Space should be 80");

//            result = new CourseViewModel(3, _context);
//            Assert.Equal("COMP 4490", result.thisCourse.courseName);
//            Assert.Equal("Computer Graphics", result.thisCourse.courseDescription);
//            Assert.True(result.thisCourse.creditHours == 3, "Credit hour shold be 3");
//            Assert.True(result.thisCourse.space == 80, "Space should be 80");
//        }

//        [Fact]
//        public void RatingObjectForConstructor()
//        {
//            var result = new CourseViewModel(1, _context);
//            Assert.Equal("N/A", result.rate);
//            result = new CourseViewModel(2, _context);
//            Assert.Equal("80/100", result.rate);
//            result = new CourseViewModel(3, _context);
//            Assert.Equal("N/A", result.rate);
//            result = new CourseViewModel(4, _context);
//            Assert.Equal("90/100", result.rate);

//        }

//        [Fact]
//        public void NullRatingObjectForConstructor()
//        {
//            var result = new CourseViewModel(5, _context);

//            Assert.True(result.thisCourse == null, "Rate should be null");

//            result = new CourseViewModel(-1, _context);

//            Assert.True(result.thisCourse == null, "Rate should be null");
//        }

//        [Fact]
//        public void CommentObjectForConstructor()
//        {
//            var result = new CourseViewModel(1, _context);
//            Assert.True(result.commentNum == 0, "Should not have comment");
//            Assert.True(result.keyParis.Count == 0, "Should nothing in there");

//            result = new CourseViewModel(2, _context);
//            Assert.True(result.commentNum == 1, "Should have 1 comment");
//            Assert.True(result.keyParis.Count == 1, "Should have 1 comment");
//            Assert.Equal("John Braico", result.keyParis.Keys.ElementAt(0));
//            Assert.Equal("Very good", result.keyParis.Values.ElementAt(0));
//        }

//    }
