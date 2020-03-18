//using cRegis.Web.Models.ViewModels;
//using cRegis.Web.test.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace cRegis.Web.Tests.UnitTests.Models.ViewModels
//{
//    public class ProfileViewModelTests : TestBase
//    {
//        [Fact]
//        public void studentObjectTest()
//        {
//            var result = new ProfileViewModel(1, _context);
//            Assert.True(result.thisStudent.studentId == 1, "student id should equal 1");
//            Assert.Equal("John Braico", result.thisStudent.name);
//            Assert.True(result.thisStudent.majorId==1, "student majorId should equal 1");

//            result = new ProfileViewModel(2, _context);
//            Assert.True(result.thisStudent.studentId == 2, "student id should equal 1");
//            Assert.Equal("Mike Zapp", result.thisStudent.name);
//            Assert.True(result.thisStudent.majorId == 2, "student majorId should equal 1");
//        }

//        [Fact]
//        public void keyPairObjectTest()
//        {
//            var result = new ProfileViewModel(1, _context);
//            Assert.True(result.keyValues.Count == 1, "student should have 2 erolls");
//            Assert.True(result.keyValues.Keys.ElementAt(0) == 1, "first enroll should be 1");
//            Assert.True(result.keyValues.Values.ElementAt(0).courseId == 1, "first course should be 1");

//            result = new ProfileViewModel(3, _context);
//            Assert.True(result.keyValues.Count == 1, "student should have 2 erolls");
//            Assert.True(result.keyValues.Keys.ElementAt(0) == 5, "first enroll should be 5");;
//            Assert.True(result.keyValues.Values.ElementAt(0).courseId == 2, "first course should be 2");
//        }

//        [Fact]
//        public void invalidInputTest()
//        {
//            var result = new ProfileViewModel(-1, _context);
//            Assert.True(result.thisStudent == null, "Student should not be found");

//            result = new ProfileViewModel(8, _context);
//            Assert.True(result.thisStudent == null, "Student should not be found");
//        }
//    }
//}
