

using cRegis.Core.DTOs;
using cRegis.Core.Entities;
using cRegis.Core.Services;
using cRegis.Web.Interfaces;
using cRegis.Web.Services;
using cRegis.Web.test.Infrastructure;
using cRegis.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.UnitTests.UnitTests.Web.Services
{
    public class ViewModelServiceTest : TestBase
    {
        private readonly IViewModelService _viewModelSerivce;

        public ViewModelServiceTest()
        {
            _viewModelSerivce = new ViewModelService(new CourseService(_context), 
                new EnrollService(_context),
                new StudentService(_context),
                new FacultyService(_context),
                new WishlistService(_context));
        }

        //TODO: write test against all method in cRegis.Web.Service.ViewModelService

        [Fact]
        public void buildCourseCommentViewModelTest()
        {
            Enrolled thisEnroll = _context.Enrolled.Find(1);
            thisEnroll.course = _context.Courses.Find(thisEnroll.courseId);
            Comment newComment = new Comment(thisEnroll);
            CourseCommentViewModel thisModel = _viewModelSerivce.buildCourseCommentViewModel(newComment);
            Assert.True(thisModel.ratingScore == 90, "rating should be 90");
            Assert.Equal("I like that course", thisModel.comment);
            Assert.True(thisModel.takenDate.Equals(new DateTime(2019, 9, 6)), "should have same date");
        }


        [Fact]
        public void buildCourseContainerViewModelTest()
        {
            Enrolled thisEnroll = _context.Enrolled.Find(1);
            Student thisStudent = _context.Students.Find(thisEnroll.studentId);
            Course thisCourse = _context.Courses.Find(thisEnroll.courseId);
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };
            CourseContainerViewModel test1 = _viewModelSerivce.buildCourseContainerViewModel(thisCourse, actions);
            Assert.True(test1.courseId == 1);
            Assert.Equal("COMP 1010", test1.courseName);
            Assert.Equal("An Introduction to Computer Science 1",test1.courseDescription);
            CourseContainerViewModel test2 = _viewModelSerivce.buildCourseContainerViewModel(thisCourse, actions,thisEnroll,thisStudent);
            Assert.True(test2.enrollId == 1);
            Assert.True(test2.studentId == 1);
        }

        [Fact]
        public void buildCourseDetailViewModelTest()
        {
            CourseDetailViewModel test = _viewModelSerivce.buildCourseDetailViewModel(1);
            Assert.Equal("COMP 1010", test.courseName);
            Assert.Equal("An Introduction to Computer Science 1", test.courseDescription);
            Assert.True(test.availableSpace == 5);
            Assert.Equal("77/100",test.avgRating);
            Assert.True(test.comments.Count() == 7);

        }

        [Fact]
        public void buildCourseDetailViewModelEdgeTest()
        {
            CourseDetailViewModel test1 = _viewModelSerivce.buildCourseDetailViewModel(-1);
            Assert.Null(test1);

            CourseDetailViewModel test2 = _viewModelSerivce.buildCourseDetailViewModel(19);
            Assert.Equal("N/A", test2.avgRating);
            Assert.Null(test2.comments);
        }

        [Fact]
        public async Task buildFindCourseViewModelTestAsync()
        {
            Student thisStudent = _context.Students.Find(1);
            FindCourseViewModel test = await _viewModelSerivce.buildFindCourseViewModel(thisStudent);
            Assert.True(test.courseList.First().courseId == 12);
            Assert.True(test.courseList.Count() == 8);
        }

        [Fact]
        public async Task buildFindCourseViewModelEdgeTestAsync()
        {
            FindCourseViewModel test = await _viewModelSerivce.buildFindCourseViewModel(null);
            Assert.Null(test);
        }

        [Fact]
        public void buildHistoryViewModelTest()
        {
            Student thisStudent = _context.Students.Find(1);
            HistoryViewModel test = _viewModelSerivce.buildHistoryViewModel(thisStudent);
            Assert.True(test.courses.First().courseId == 1);
            Assert.True(test.courses.Count() == 12);
        }

        [Fact]
        public void buildHistoryViewModelEdgeTest()
        {
            HistoryViewModel test = _viewModelSerivce.buildHistoryViewModel(null);
            Assert.Null(test);
        }

        [Fact]
        public async Task buildProfileViewModelTestAsync()
        {
            Student thisStudent = _context.Students.Find(1);
            ProfileViewModel result = await _viewModelSerivce.buildProfileViewModel(thisStudent);
            Assert.True(result.thisStudent.studentId == 1, "student id should equal 1");
            Assert.Equal("John Braico", result.thisStudent.name);
            Assert.Equal("Computer Science", result.majorName);
            Assert.True(result.cViewModels.Count() == 0, "student should have 0 erolls");
            Assert.True(result.remainingCreditHours==24, "student should have 24 credit hours remaining");


            thisStudent = _context.Students.Find(2);
            result = await _viewModelSerivce.buildProfileViewModel(thisStudent);
            Assert.True(result.thisStudent.studentId == 2, "student id should equal 1");
            Assert.Equal("Mike Zapp", result.thisStudent.name);
            Assert.Equal("Computer Science", result.majorName);
            Assert.True(result.cViewModels.Count() == 1, "student should have 1 eroll");
            Assert.True(result.cViewModels.First().enrollId == 23);
            Assert.True(result.remainingCreditHours == 30, "student should have 30 credit hours remaining");
        }

        [Fact]
        public async Task buildProfileViewModelEdgeTestAsync()
        {
            ProfileViewModel result = await _viewModelSerivce.buildProfileViewModel(null);
            Assert.Null(result);
        }

        [Fact]
        public void buildRateCourseViewModelTest()
        {
            Enrolled thisEnroll = _context.Enrolled.Find(9);
            Course thisCourse = _context.Courses.Find(thisEnroll.courseId);
            RateCourseViewModel thisModel = _viewModelSerivce.buildRateCourseViewModel(thisEnroll, thisCourse);
            Assert.True(thisModel.EnrollId == 9);
            Assert.Null(thisModel.Rating);
            Assert.Null(thisModel.Comment);
            Assert.Equal("COMP 3350", thisModel.CourseName);
            Assert.Equal("Software Engineer 1", thisModel.courseDescription);
        }
    }
}
