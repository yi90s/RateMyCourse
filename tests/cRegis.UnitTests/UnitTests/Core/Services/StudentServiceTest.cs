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

        [Fact]
        public async void getStudentAsyncTest()
        {
            Student stu = await _studentService.getStudentAsync(1);
            Student stu2 = await _studentService.getStudentAsync(2);
            Assert.NotNull(stu);
            Assert.NotNull(stu2);
            Assert.Equal("John Braico", stu.name);
            Assert.True(stu.majorId == 1);
            Assert.Equal("Mike Zapp", stu2.name);
            Assert.True(stu2.majorId == 1);
        }

        [Fact]
        public void getRemainingCredithoursForStudentTest()
        {
            int remaingHour_stu = _studentService.getRemainingCredithoursForStudent(1);
            int remaingHour_stu2 = _studentService.getRemainingCredithoursForStudent(3);

            Assert.True(remaingHour_stu == 24);
            Assert.True(remaingHour_stu2 == 48);
        }

        [Fact]
        public void registerCourseForStudentTest()
        {
            _studentService.registerCourseForStudent(3, 5);
            List<int> registerCourses = _context.Enrolled.Where(e => !e.completed && e.studentId == 3).Select(e => e.courseId).ToList();
            Assert.True(registerCourses.Contains(5), "student registed course 3");

        }

        [Fact]
        public void verifyDropForStudentTest()
        {
            _studentService.verifyDropForStudent(3, 5);
            List<int> registerCourses = _context.Enrolled.Where(e => !e.completed && e.studentId == 3).Select(e => e.courseId).ToList();
            Assert.False(registerCourses.Contains(5), "student drops course 3");

        }
        [Fact]
        public async void verifyRegistrationForStudentTest()
        {
            bool result1 = await _studentService.verifyRegistrationForStudent(2, 5);
            bool result2 = await _studentService.verifyRegistrationForStudent(3, 5);
            bool result3 = await _studentService.verifyRegistrationForStudent(3, 15);
            bool result4 = await _studentService.verifyRegistrationForStudent(4, 15);
            Assert.True(result1, "student is able to register this course");
            Assert.True(result2, "student is able to register this course");
            Assert.False(result3, "student does not have Prerequisite");
            Assert.False(result4, "student does not have Prerequisite");

        }


    }
}
