using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class BuildCourseContainerViewModelTests : TestBase
    {
        private readonly IViewModelService _viewModelService;

        public BuildCourseContainerViewModelTests()
        {
            _viewModelService = new ViewModelService(new CourseService(_context),
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context));
        }

        //buildCourseContainerViewModelTest
        [Fact]
        public void buildCourseContainerViewModelTest_HappyPath()
        {
            Enrolled tempEnrolled = _context.Enrolled.Find(1);
            Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
            Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };

            CourseContainerViewModel courseContainerViewModel1 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions);
            Assert.True(courseContainerViewModel1.courseId == 1);
            Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
            Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);

            CourseContainerViewModel courseContainerViewModel2 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
            Assert.True(courseContainerViewModel2.enrollId == 1);
            Assert.True(courseContainerViewModel2.studentId == 1);
        }

        [Fact]
        public void buildCourseContainerViewModelTest_NullCourse()
        {
            Enrolled tempEnrolled = _context.Enrolled.Find(1);
            Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
            Course tempCourse = null;
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };
            CourseContainerViewModel courseContainerViewModel1 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions);
            Assert.Null(courseContainerViewModel1);
            CourseContainerViewModel courseContainerViewModel2 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
            Assert.Null(courseContainerViewModel2);
        }

        [Fact]
        public void buildCourseContainerViewModelTest_NullActions()
        {
            Enrolled tempEnrolled = _context.Enrolled.Find(1);
            Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
            Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
            ISet<CourseActions> actions = null;
            CourseContainerViewModel courseContainerViewModel1 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions);

            Assert.True(courseContainerViewModel1.courseId == 1);
            Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
            Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);
            Assert.Null(courseContainerViewModel1.actions);

            CourseContainerViewModel courseContainerViewModel2 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
            Assert.True(courseContainerViewModel2.enrollId == 1);
            Assert.True(courseContainerViewModel2.studentId == 1);
            Assert.Null(courseContainerViewModel1.actions);
        }

        [Fact]
        public void buildCourseContainerViewModelTest_EmptyActions()
        {
            Enrolled tempEnrolled = _context.Enrolled.Find(1);
            Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
            Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
            ISet<CourseActions> actions = new HashSet<CourseActions>();
            Assert.True(actions.Count == 0);

            CourseContainerViewModel courseContainerViewModel1 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions);
            Assert.True(courseContainerViewModel1.courseId == 1);
            Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
            Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);
            Assert.True(courseContainerViewModel1.actions.Count == 0);

            CourseContainerViewModel courseContainerViewModel2 = _viewModelService.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
            Assert.True(courseContainerViewModel2.enrollId == 1);
            Assert.True(courseContainerViewModel2.studentId == 1);
        }
    }
}
