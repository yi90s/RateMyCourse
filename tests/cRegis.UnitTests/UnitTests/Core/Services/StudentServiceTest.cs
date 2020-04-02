using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.test.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Core.Services
{
    public class StudentServiceTest : TestBase
    {
        private readonly IStudentService _studentService;

        public StudentServiceTest()
        {
            _studentService = new StudentService(_context);
        }

        [Fact]
        public async void nullFindStudentById()
        {
            Student c = await _studentService.getStudentAsync(-1);
            Assert.Null(c);
        }

        //getStudentAsyncTest()
        [Fact]
        public async void getStudentAsyncTest_HappyPath()
        {
            Student student = await _studentService.getStudentAsync(1);
            Assert.NotNull(student);
            Assert.Equal("John Braico", student.name);
            Assert.True(student.majorId == 1);
        }

        [Fact]
        public async void getStudentAsyncTest_NonExistantStudent()
        {
            Student student = await _studentService.getStudentAsync(-1);
            Assert.Null(student);
        }

        //getRemainingCredithoursForStudentTest()
        [Fact]
        public void getRemainingCredithoursForStudentTest_HappyPath()
        {
            int outcome = _studentService.getRemainingCredithoursForStudent(1);
            Assert.True(outcome == 24);
        }

        [Fact]
        public void getRemainingCredithoursForStudentTest_NonExistantStudent()
        {
            int outcome = _studentService.getRemainingCredithoursForStudent(-1);
            Assert.True(outcome == -1);
        }

        //registerCourseForStudentTest()
        [Fact]
        public async void registerCourseForStudentTest_HappyPath()
        {
            int outcome = await _studentService.registerCourseForStudent(3, 5);
            Assert.True(outcome == 0);
            List<int> registerCourses = _context.Enrolled.Where(e => !e.completed && e.studentId == 3).Select(e => e.courseId).ToList();
            Assert.True(registerCourses.Contains(5), "student registed course 3");
        }

        [Fact]
        public async void registerCourseForStudentTest_FalseOutcome()
        {
            int outcome = await _studentService.registerCourseForStudent(3, 5);
            Assert.True(outcome == 0);
            List<int> registerCourses = _context.Enrolled.Where(e => !e.completed && e.studentId == -1).Select(e => e.courseId).ToList();
            Assert.False(registerCourses.Contains(5), "student registed course 3");
        }

        [Fact]
        public async void registerCourseForStudentTest_NonExistantStudent()
        {
            int outcome = await _studentService.registerCourseForStudent(-1, 5);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void registerCourseForStudentTest_NonExistantCourse()
        {
            int outcome = await _studentService.registerCourseForStudent(3, -1);
            Assert.True(outcome == 2);
        }

        //verifyDropForStudentTest()
        [Fact]
        public async void verifyDropForStudentTest_HappyPath()
        {
            int studentId = 1;
            int enrolledId = 5;
            int temp = _context.Enrolled.Find(enrolledId).studentId;
            Assert.True(studentId == temp);
            int outcome = await _studentService.verifyDropForStudent(1, 5);
            Assert.True(outcome == 0);
        }

        [Fact]
        public async void verifyDropForStudentTest_FalseOutcome()
        {
            int studentId = 2;
            int enrolledId = 5;
            int temp = _context.Enrolled.Find(enrolledId).studentId;
            Assert.False(studentId == temp);
            int outcome = await _studentService.verifyDropForStudent(2, 5);
            Assert.True(outcome == -1);
        }

        [Fact]
        public async void verifyDropForStudentTest_NonExistantStudent()
        {
            int outcome = await _studentService.verifyDropForStudent(-1, 5);
            Assert.True(outcome == -1);
        }

        [Fact]
        public async void verifyDropForStudentTest_NonExistantEnroll()
        {
            int outcome = await _studentService.verifyDropForStudent(3, -1);
            Assert.True(outcome == 1);
        }

        //verifyRegistrationForStudentTest()
        [Fact]
        public async void verifyRegistrationForStudentTest_HappyPath()
        {
            int outcome1 = await _studentService.verifyRegistrationForStudent(2, 5);
            Assert.True(outcome1 == 0);
        }

        [Fact]
        public async void verifyRegistrationForStudentTest_FalseOutcome()
        {
            int outcome2 = await _studentService.verifyRegistrationForStudent(3, 15);
            Assert.True(outcome2 == -1);
        }

        [Fact]
        public async void verifyRegistrationForStudentTest_NonExistantStudent()
        {
            int outcome = await _studentService.verifyRegistrationForStudent(-1, 5);
            Assert.True(outcome == 1);
        }

        [Fact]
        public async void verifyRegistrationForStudentTest_NonExistantCourse()
        {
            int outcome = await _studentService.verifyRegistrationForStudent(2, -1);
            Assert.True(outcome == 2);
        }
    }
}
