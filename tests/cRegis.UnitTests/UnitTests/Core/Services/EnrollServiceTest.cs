using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using System.Collections.Generic;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{

    public class EnrollServiceTest : TestBase
    {
        private readonly IEnrollService _enrollService;
        public EnrollServiceTest()
        {
            _enrollService = new EnrollService(_context);
        }

        [Fact]
        public async void nullFindEnrollById()
        {
            Enrolled c = await _enrollService.getEnrollAsync(-1);
            Assert.Null(c);
        }

        //TODO : writting test against all the methods in cRegis.Core.Service.EnrollService

        //GetEnrollAsyncTest()
        [Fact]
        public async void GetEnrollAsyncTest_HappyPath()
        {
            Enrolled enrolled = await _enrollService.getEnrollAsync(1);
            Assert.NotNull(enrolled);
            //Verify that enrolled is what it should be
            Assert.True(enrolled.enrollId == 1);
            Assert.True(enrolled.courseId == 1);
            Assert.True(enrolled.studentId == 1);
            Assert.True(enrolled.completed);
            Assert.True(enrolled.grade == 80);
            Assert.True(enrolled.rating == 90);
            Assert.Equal("I like that course", enrolled.comment);
            Assert.Null(enrolled.course);
            Assert.Null(enrolled.student);
        }

        [Fact]
        public async void GetEnrollAsyncTest_NonExistantEnroll()
        {
            Enrolled enrolled = await _enrollService.getEnrollAsync(-1);
            Assert.Null(enrolled);
        }

        //GetCompletedEnrollsForStudentTest()
        [Fact]
        public void GetCompletedEnrollsForStudentTest_HappyPath()
        {
            List<Enrolled> enrolledList = _enrollService.getCompletedEnrollsForStudent(5);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count > 0);
            //Check the enrollId and courseId for each element that should be in the list
            Assert.True(enrolledList.Count == 4);
            //33
            Assert.True(enrolledList[0].enrollId == 34);
            Assert.True(enrolledList[0].courseId == 1);
            //34
            Assert.True(enrolledList[1].enrollId == 35);
            Assert.True(enrolledList[1].courseId == 2);
            //35
            Assert.True(enrolledList[2].enrollId == 36);
            Assert.True(enrolledList[2].courseId == 3);
            //36
            Assert.True(enrolledList[3].enrollId == 37);
            Assert.True(enrolledList[3].courseId == 4);
        }

        [Fact]
        public void GetCompletedEnrollsForStudentTest_NonExistantStudent()
        {
            List<Enrolled> enrolledList = _enrollService.getCompletedEnrollsForStudent(-1);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        [Fact]
        public void GetCompletedEnrollsForStudentTest_StudentWithoutEnrolls()
        {
            List<Enrolled> enrolledList = _enrollService.getCompletedEnrollsForStudent(8);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        //GetCurrentEnrollsForStudentTest()
        [Fact]
        public void GetCurrentEnrollsForStudentTest_HappyPath()
        {
            List<Enrolled> enrolledList = _enrollService.getCurrentEnrollsForStudent(5);
            Assert.NotNull(enrolledList);
            //Verify that the list contains only the enrollments that it should
            Assert.True(enrolledList.Count == 1);
            Assert.True(enrolledList[0].enrollId == 38);
            Assert.True(enrolledList[0].courseId == 6);
        }

        [Fact]
        public void GetCurrentEnrollsForStudentTest_NonExistantStudent()
        {
            List<Enrolled> enrolledList = _enrollService.getCurrentEnrollsForStudent(-1);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        [Fact]
        public void GetCurrentEnrollsForStudentTest_StudentWithoutCurrentEnrolls()
        {
            List<Enrolled> enrolledList = _enrollService.getCurrentEnrollsForStudent(8);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        //GetEnrollsForStudentTest()
        [Fact]
        public void GetEnrollsForStudentTest_HappyPath()
        {
            List<Enrolled> enrolledList = _enrollService.getEnrollsForStudent(1);
            Assert.NotNull(enrolledList);
            //Check the enrollId and courseId for each element in the list
            Assert.True(enrolledList.Count == 12);
            //0
            Assert.True(enrolledList[0].enrollId == 1);
            Assert.True(enrolledList[0].courseId == 1);
            //1
            Assert.True(enrolledList[1].enrollId == 2);
            Assert.True(enrolledList[1].courseId == 2);
            //2
            Assert.True(enrolledList[2].enrollId == 3);
            Assert.True(enrolledList[2].courseId == 3);
            //3
            Assert.True(enrolledList[3].enrollId == 4);
            Assert.True(enrolledList[3].courseId == 4);
            //4
            Assert.True(enrolledList[4].enrollId == 5);
            Assert.True(enrolledList[4].courseId == 5);
            //5
            Assert.True(enrolledList[5].enrollId == 6);
            Assert.True(enrolledList[5].courseId == 6);
            //6
            Assert.True(enrolledList[6].enrollId == 7);
            Assert.True(enrolledList[6].courseId == 7);
            //7
            Assert.True(enrolledList[7].enrollId == 8);
            Assert.True(enrolledList[7].courseId == 8);
            //8
            Assert.True(enrolledList[8].enrollId == 9);
            Assert.True(enrolledList[8].courseId == 9);
            //9
            Assert.True(enrolledList[9].enrollId == 10);
            Assert.True(enrolledList[9].courseId == 11);
            //10
            Assert.True(enrolledList[10].enrollId == 11);
            Assert.True(enrolledList[10].courseId == 15);
            //11
            Assert.True(enrolledList[11].enrollId == 12);
            Assert.True(enrolledList[11].courseId == 14);
        }

        [Fact]
        public void GetEnrollsForStudentTest_NonExistantStudent()
        {
            List<Enrolled> enrolledList = _enrollService.getEnrollsForStudent(-1);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        [Fact]
        public void GetEnrollsForStudentTest_StudentWithoutCurrentEnrolls()
        {
            List<Enrolled> enrolledList = _enrollService.getEnrollsForStudent(8);
            Assert.NotNull(enrolledList);
            Assert.True(enrolledList.Count == 0);
        }

        //UpdateEnrollTest()
        [Fact]
        public async void UpdateEnrollTest_HappyPath()
        {
            int enrollNum = 1;
            Enrolled enrolled = await _enrollService.getEnrollAsync(enrollNum);
            Assert.NotNull(enrolled);
            //Make changes
            enrolled.courseId = 99;
            enrolled.studentId = 99;
            enrolled.completed = true;
            enrolled.grade = 99;
            enrolled.rating = 99;
            enrolled.comment = "";
            enrolled.course = null;
            enrolled.student = null;
            //Update the enrolled
            _enrollService.updateEnroll(enrolled);
            Enrolled enrolledTemp = await _enrollService.getEnrollAsync(enrollNum);
            Assert.NotNull(enrolledTemp);
            //Verify that the changes took place
            Assert.True(enrolledTemp.enrollId == 1);
            Assert.True(enrolledTemp.studentId == 99);
            Assert.True(enrolledTemp.completed);
            Assert.True(enrolledTemp.grade == 99);
            Assert.True(enrolledTemp.rating == 99);
            Assert.Equal("", enrolledTemp.comment);
            Assert.Null(enrolledTemp.course);
            Assert.Null(enrolledTemp.student);
        }

        [Fact]
        public async void UpdateEnrollTest_NullEnroll()
        {
            int enrollNum = -1;
            Enrolled enrolled = await _enrollService.getEnrollAsync(enrollNum);
            Assert.Null(enrolled);
            //Update the enrolled
            int outcome = _enrollService.updateEnroll(enrolled);
            Assert.True(outcome == 1);
        }

        [Fact]
        public void UpdateEnrollTest_NonExistantEnroll()
        {
            Enrolled enrolled = new Enrolled { enrollId = 99, studentId = 8, courseId = 0, completed = true, grade = 99, rating = 99, comment = "" };
            Assert.NotNull(enrolled);
            //Update the enrolled
            int outcome = _enrollService.updateEnroll(enrolled);
            Assert.True(outcome == 2);
        }

        //dropTest()
        [Fact]
        public async void dropTest_HappyPath()
        {
            int enrollNum = 1;
            //Get the enroll at the enrollNum
            Enrolled enrolled = await _enrollService.getEnrollAsync(enrollNum);
            Assert.NotNull(enrolled);
            //Ensure that enrolled is the correct object
            Assert.True(enrolled.enrollId == 1);
            Assert.True(enrolled.courseId == 1);
            Assert.True(enrolled.studentId == 1);
            Assert.True(enrolled.completed);
            Assert.True(enrolled.grade == 80);
            Assert.True(enrolled.rating == 90);
            Assert.Equal("I like that course", enrolled.comment);
            Assert.Null(enrolled.course);
            Assert.Null(enrolled.student);
            //Drop enroll at enrollNum
            _enrollService.drop(enrollNum);
            Enrolled enrolledTemp = await _enrollService.getEnrollAsync(enrollNum);
            //Check if that enrollNum exists
            Assert.Null(enrolledTemp);
        }

        [Fact]
        public void dropTest_NonExistantEnroll()
        {
            Enrolled outcome = _enrollService.drop(-1);
            Assert.Null(outcome);
        }
    }
}
