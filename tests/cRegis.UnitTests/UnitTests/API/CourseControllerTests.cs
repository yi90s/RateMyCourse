using cRegis.API.Controllers;
using cRegis.Core.Entities;
using cRegis.UnitTests.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.UnitTests.UnitTests.API
{
    public class CourseControllerTests:APITestBase
    {
        private CourseController _controller;
        public CourseControllerTests():base()
        {
            _controller = new CourseController(_courseService);
        }

        [Theory]
        [InlineData("COMP",19)]
        [InlineData("MATH", 2)]
        [InlineData("STAT", 2)]
        public void getCoursesByKeywordsTest(string keyWords,int num)
        {
            var result = _controller.getCoursesByKeywords(keyWords);
            Assert.True(result.Value.Count == num);
        }

        [Theory]
        [InlineData(1, 8)]
        [InlineData(2, 6)]
        [InlineData(3, 7)]
        [InlineData(4, 5)]
        public void getCoursesInYearTest(int year, int num)
        {
            var result = _controller.getCoursesInYear(year);
            Assert.True(result.Value.Count == num);
        }


        [Theory]
        [InlineData(1,8)]
        [InlineData(2,10)]
        [InlineData(3,13)]
        public async Task getRecCourseForStudentTest(int id,int num)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             })) ;
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.getRecCoursesForStudentAsync();
            Assert.True(result.Value.Count == num);
        }

        [Theory]
        [InlineData(1, 26)]
        [InlineData(2, 26)]
        [InlineData(3, 26)]
        public async Task getEligibleCourseForStudentTest(int id, int num)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.getEligibleCoursesForStudentAsync();
            Assert.True(result.Value.Count == num);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        public async Task getTakingCoursesTests(int id, int num)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await _controller.getTakingCourses();
            Assert.True(result.Value.Count == num);
        }

        [Theory]
        [InlineData(3, 4)]
        [InlineData(4, 4)]
        [InlineData(5, 4)]
        public void getCompletedCoursesTest(int id, int num)
        {
            //Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
             {
                    new Claim("sid",id.ToString())
             }));
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result =  _controller.getCompletedCourses();
            Assert.True(result.Value.Count == num);
        }


        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task getCourseAsyncTest(int cid)
        {
            var result = await _controller.getCourseAsync(cid);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(1, 7, "I like that course", 90)]
        [InlineData(2, 7, "Very good", 80)]
        [InlineData(3, 2, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand", 75)]
        public async Task getCommentsForCourse(int courseId, int commentCountExpected, string expectedComment, int expectedRating)
        {
            var commentList = ( _controller.getCommentsForCourse(courseId)).Value;
            Assert.NotEmpty(commentList);
            var numCommentsFound = commentList.Count;
            Assert.Equal(commentCountExpected, numCommentsFound);
            Assert.True(commentList.Exists(c => c.comment.Equals(expectedComment)));
            Assert.True(commentList.Exists(c => c.ratingScore.Equals(expectedRating)));
        }
    }
}
