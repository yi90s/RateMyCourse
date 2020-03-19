//using cRegis.Web.test.Infrastructure;
//using Xunit;
//using cRegis.Web.Services;
//using System.Linq;
//using cRegis.Web.Models.entities;
//using cRegis.Web.Models.ViewModels;
//using System;

//namespace cRegis.Web.Tests.UnitTests.Services
//{
//    public class ServicesTests : TestBase
//    {
//        private Service services;
//        public ServicesTests()
//        {
//            services = new Service(_context);
//        }

//        [Fact]
//        public async void normalFindStudentByIdTest()
//        {
//            Student stu = await services.findStudentById(1);

//            Assert.NotNull(stu);
//            Assert.Equal(1, stu.studentId);
//            Assert.Equal("John Braico", stu.name);
//            Assert.Equal(1, stu.majorId);

//        }

//        [Fact]
//        public async void nullFindStudentByIdTest()
//        {
//            Student stu = await services.findStudentById(-1);

//            Assert.Null(stu);
//        }

//        [Fact]
//        public async void normalFindCourseById()
//        {
//            Models.entities.Course c = await services.findCourseById(1);

//            Assert.NotNull(c);
//            Assert.Equal(1, c.courseId);
//            Assert.Equal("COMP 4380", c.courseName);
//            Assert.Equal("Database Implementation", c.courseDescription);
//            Assert.Equal(3, c.creditHours);
//            Assert.Equal(80, c.space);
//            Assert.Equal(new DateTime(2019, 9, 6), c.date);

//        }

//        [Fact]
//        public async void nullFindCourseById()
//        {
//            Models.entities.Course c = await services.findCourseById(-1);

//            Assert.Null(c);
//        }

//        [Fact]
//        public void normalCreateCourseViewModelTest()
//        {
//            var result = services.createCourseViewModel(1);

//            Assert.Equal("COMP 4380", result.thisCourse.courseName);
//            Assert.Equal("Database Implementation", result.thisCourse.courseDescription);
//            Assert.True(result.thisCourse.creditHours == 3, "Credit hour shold be 3");
//            Assert.True(result.thisCourse.space == 80, "Space should be 80");
//            Assert.Equal("N/A", result.rate);
//            Assert.True(result.commentNum == 0, "Should not have comment");
//            Assert.True(result.keyParis.Count == 0, "Should nothing in there");

//            result = services.createCourseViewModel(2);
//            Assert.Equal("80/100", result.rate);
//            Assert.True(result.commentNum == 1, "Should have 1 comment");
//            Assert.True(result.keyParis.Count == 1, "Should have 1 comment");
//            Assert.Equal("John Braico", result.keyParis.Keys.ElementAt(0));
//            Assert.Equal("Very good", result.keyParis.Values.ElementAt(0));

//            result = services.createCourseViewModel(3);
//            Assert.Equal("COMP 4490", result.thisCourse.courseName);
//            Assert.Equal("Computer Graphics", result.thisCourse.courseDescription);
//            Assert.True(result.thisCourse.creditHours == 3, "Credit hour shold be 3");
//            Assert.True(result.thisCourse.space == 80, "Space should be 80");
//            Assert.Equal("N/A", result.rate);
//            Assert.True(result.enrollId == -1, "should not have enroll id");

//            Enrolled newEnroll = _context.Enrolled.Find(1);
//            result = services.createCourseViewModel(1, newEnroll);
//            Assert.True(result.enrollId == 1, "should have enroll id");
//        }
//        [Fact]
//        public void nullCreateCourseViewModelTest()
//        {
//            var result = services.createCourseViewModel(-1);
//            Assert.True(result == null, "object should be null");

//            result = services.createCourseViewModel(5);
//            Assert.True(result == null, "object should be null");
//        }
//        //[Fact]
//        //public async void normalCreateProfileViewModelTest()
//        //{
//        //    Student stu = _context.Students.Find(1);
//        //    ProfileViewModel result = await services.createProfileViewModel(stu).ConfigureAwait(false);
//        //    Assert.True(result.thisStudent.studentId==1, "student id should equal 1");
//        //    Assert.Equal("John Braico", result.thisStudent.name);
//        //    Assert.True(result.thisStudent.majorId == 1, "student majorId should equal 1");
//        //    Assert.True(result.keyValues.Count == 1, "student should have 2 erolls");
//        //    Assert.True(result.keyValues.Keys.ElementAt(0) == 1, "first enroll should be 1");
//        //    Assert.True(result.keyValues.Values.ElementAt(0).courseId == 1, "first course should be 1");

//        //    stu = _context.Students.Find(2);
//        //    result = await services.createProfileViewModel(stu).ConfigureAwait(false);
//        //    Assert.True(result.thisStudent.studentId == 2, "student id should equal 2");
//        //    Assert.Equal("Mike Zapp", result.thisStudent.name);
//        //    Assert.True(result.thisStudent.majorId == 2, "student majorId should equal 2");

//        //    stu = _context.Students.Find(3);
//        //    result = await services.createProfileViewModel(stu).ConfigureAwait(false);
//        //    Assert.True(result.keyValues.Count == 1, "student should have 2 erolls");
//        //    Assert.True(result.keyValues.Keys.ElementAt(0) == 5, "first enroll should be 5"); ;
//        //    Assert.True(result.keyValues.Values.ElementAt(0).courseId == 2, "first course should be 2");
//        //}

//        [Fact]
//        public async void nullCreateProfileViewModelTest()
//        {
//            ProfileViewModel result = await services.createProfileViewModel(null).ConfigureAwait(false);
//            Assert.True(result.thisStudent == null, "Student should not be found");

//            result = await services.createProfileViewModel(new Student { studentId = 5, name = "test", majorId = 1 }).ConfigureAwait(false);
//            Assert.True(result.thisStudent == null, "Student should not be found");
//        }

//        [Fact]
//        public async void verifyDropForStudentTest()
//        {

//        }
//    }
//}
