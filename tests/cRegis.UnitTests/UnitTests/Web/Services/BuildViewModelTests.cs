//using cRegis.Core.Services;
//using cRegis.Web.Interfaces;
//using cRegis.Web.Services;
//using cRegis.Web.test.Infrastructure;

//namespace cRegis.UnitTests.UnitTests.Web.Services
//{
//    public class BuildViewModelTests : TestBase
//    {
//        private readonly IViewModelService _viewModelService;

//        public BuildViewModelTests()
//        {
//            _viewModelService = new ViewModelService(new CourseService(_context),
//                new EnrollService(_context),
//                new StudentService(_context),
//                new FacultyService(_context));
//        }
//    }
//}

////TODO: write test against all method in cRegis.Web.Service.ViewModelService

////    //buildCourseCommentViewModelTest
////    [Fact]
////    public void buildCourseCommentViewModelTest_HappyPath()
////    {
////        Enrolled tempEnrolled = _context.Enrolled.Find(1);
////        tempEnrolled.course = _context.Courses.Find(tempEnrolled.courseId);
////        Comment testComment = new Comment(tempEnrolled);
////        CourseCommentViewModel courseCommentViewModel = _viewModelSerivce.buildCourseCommentViewModel(testComment);
////        Assert.True(courseCommentViewModel.ratingScore == 90, "rating should be 90");
////        Assert.Equal("I like that course", courseCommentViewModel.comment);
////        Assert.True(courseCommentViewModel.takenDate.Equals(new DateTime(2019, 9, 6)), "should have same date");
////    }

////    [Fact]
////    public void buildCourseCommentViewModelTest_NullParameter()
////    {
////        Comment testComment = null;
////        CourseCommentViewModel courseCommentViewModel = _viewModelSerivce.buildCourseCommentViewModel(testComment);
////        Assert.Null(courseCommentViewModel);
////    }

////    //buildCourseContainerViewModelTest
////    [Fact]
////    public void buildCourseContainerViewModelTest_HappyPath()
////    {
////        Enrolled tempEnrolled = _context.Enrolled.Find(1);
////        Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
////        Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
////        ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };

////        CourseContainerViewModel courseContainerViewModel1 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions);
////        Assert.True(courseContainerViewModel1.courseId == 1);
////        Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
////        Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);

////        CourseContainerViewModel courseContainerViewModel2 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
////        Assert.True(courseContainerViewModel2.enrollId == 1);
////        Assert.True(courseContainerViewModel2.studentId == 1);
////    }

////    [Fact]
////    public void buildCourseContainerViewModelTest_NullCourse()
////    {
////        Enrolled tempEnrolled = _context.Enrolled.Find(1);
////        Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
////        Course tempCourse = null;
////        ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };
////        CourseContainerViewModel courseContainerViewModel1 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions);
////        Assert.Null(courseContainerViewModel1);
////        CourseContainerViewModel courseContainerViewModel2 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
////        Assert.Null(courseContainerViewModel2);
////    }

////    [Fact]
////    public void buildCourseContainerViewModelTest_NullActions()
////    {
////        Enrolled tempEnrolled = _context.Enrolled.Find(1);
////        Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
////        Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
////        ISet<CourseActions> actions = null;
////        CourseContainerViewModel courseContainerViewModel1 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions);

////        Assert.True(courseContainerViewModel1.courseId == 1);
////        Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
////        Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);
////        Assert.Null(courseContainerViewModel1.actions);

////        CourseContainerViewModel courseContainerViewModel2 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
////        Assert.True(courseContainerViewModel2.enrollId == 1);
////        Assert.True(courseContainerViewModel2.studentId == 1);
////        Assert.Null(courseContainerViewModel1.actions);
////    }

////    [Fact]
////    public void buildCourseContainerViewModelTest_EmptyActions()
////    {
////        Enrolled tempEnrolled = _context.Enrolled.Find(1);
////        Student tempStudent = _context.Students.Find(tempEnrolled.studentId);
////        Course tempCourse = _context.Courses.Find(tempEnrolled.courseId);
////        ISet<CourseActions> actions = new HashSet<CourseActions>();
////        Assert.True(actions.Count == 0);

////        CourseContainerViewModel courseContainerViewModel1 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions);
////        Assert.True(courseContainerViewModel1.courseId == 1);
////        Assert.Equal("COMP 1010", courseContainerViewModel1.courseName);
////        Assert.Equal("An Introduction to Computer Science 1", courseContainerViewModel1.courseDescription);
////        Assert.True(courseContainerViewModel1.actions.Count == 0);

////        CourseContainerViewModel courseContainerViewModel2 = _viewModelSerivce.buildCourseContainerViewModel(tempCourse, actions, tempEnrolled, tempStudent);
////        Assert.True(courseContainerViewModel2.enrollId == 1);
////        Assert.True(courseContainerViewModel2.studentId == 1);
////    }

////    //buildCourseDetailViewModelTest
////    [Fact]
////    public void buildCourseDetailViewModelTest_HappyPath()
////    {
////        CourseDetailViewModel courseDetailViewModel = _viewModelSerivce.buildCourseDetailViewModel(1);
////        Assert.Equal("COMP 1010", courseDetailViewModel.courseName);
////        Assert.Equal("An Introduction to Computer Science 1", courseDetailViewModel.courseDescription);
////        Assert.True(courseDetailViewModel.availableSpace == 5);
////        Assert.Equal("77/100", courseDetailViewModel.avgRating);
////        Assert.True(courseDetailViewModel.comments.Count() == 7);
////    }

////    [Fact]
////    public void buildCourseDetailViewModelTest_NonExistantCid()
////    {
////        CourseDetailViewModel courseDetailViewModel = _viewModelSerivce.buildCourseDetailViewModel(-1);
////        Assert.Null(courseDetailViewModel);
////    }

////    [Fact]
////    public void buildCourseDetailViewModelTest_NoComments()
////    {
////        CourseDetailViewModel courseDetailViewModel = _viewModelSerivce.buildCourseDetailViewModel(19);
////        Assert.Equal("N/A", courseDetailViewModel.avgRating);
////        Assert.Null(courseDetailViewModel.comments);
////    }

////    //buildFindCourseViewModelTestAsync
////    [Fact]
////    public async void buildFindCourseViewModelTestAsync_HappyPath()
////    {
////        Student student = _context.Students.Find(1);
////        FindCourseViewModel findCourseViewModel = await _viewModelSerivce.buildFindCourseViewModelAsync(student);
////        Assert.True(findCourseViewModel.courseList.First().courseId == 12);
////        Assert.True(findCourseViewModel.courseList.Count() == 8);
////    }

////    [Fact]
////    public async void buildFindCourseViewModelTestAsync_NullParameter()
////    {
////        FindCourseViewModel test = await _viewModelSerivce.buildFindCourseViewModelAsync(null);
////        Assert.Null(test);
////    }

////    //buildHistoryViewModelTest
////    [Fact]
////    public void buildHistoryViewModelTest_HappyPath()
////    {
////        Student student = _context.Students.Find(1);
////        HistoryViewModel historyViewModel = _viewModelSerivce.buildHistoryViewModel(student);
////        Assert.True(historyViewModel.courses.First().courseId == 1);
////        Assert.True(historyViewModel.courses.Count() == 12);
////    }

////    [Fact]
////    public void buildHistoryViewModelTest_NullParameter()
////    {
////        HistoryViewModel historyViewModel = _viewModelSerivce.buildHistoryViewModel(null);
////        Assert.Null(historyViewModel);
////    }

////    //buildProfileViewModelTestAsync
////    [Fact]
////    public void buildProfileViewModelTest_HappyPath()
////    {
////        Student student = _context.Students.Find(1);
////        ProfileViewModel profileViewModel = _viewModelSerivce.buildProfileViewModel(student);
////        Assert.True(profileViewModel.thisStudent.studentId == 1, "student id should equal 1");
////        Assert.Equal("John Braico", profileViewModel.thisStudent.name);
////        Assert.Equal("Computer Science", profileViewModel.majorName);
////        Assert.True(profileViewModel.cViewModels.Count() == 0, "student should have 0 erolls");
////        Assert.True(profileViewModel.remainingCreditHours == 24, "student should have 24 credit hours remaining");
////    }

////    [Fact]
////    public void buildProfileViewModelTest_NullParameter()
////    {
////        ProfileViewModel result = _viewModelSerivce.buildProfileViewModel(null);
////        Assert.Null(result);
////    }

////    //buildRateCourseViewModelTest
////    [Fact]
////    public void buildRateCourseViewModelTest_HappyPath()
////    {
////        Enrolled enrolled = _context.Enrolled.Find(9);
////        Course course = _context.Courses.Find(enrolled.courseId);
////        RateCourseViewModel rateCourseViewModel = _viewModelSerivce.buildRateCourseViewModel(enrolled, course);
////        Assert.True(rateCourseViewModel.EnrollId == 9);
////        Assert.Null(rateCourseViewModel.Rating);
////        Assert.Null(rateCourseViewModel.Comment);
////        Assert.Equal("COMP 3350", rateCourseViewModel.CourseName);
////        Assert.Equal("Software Engineer 1", rateCourseViewModel.courseDescription);
////    }

////    [Fact]
////    public void buildRateCourseViewModelTest_NullEnrolled()
////    {
////        Course course = _context.Courses.Find(_context.Enrolled.Find(9).courseId);
////        RateCourseViewModel rateCourseViewModel = _viewModelSerivce.buildRateCourseViewModel(null, course);
////        Assert.Null(rateCourseViewModel);
////    }

////    [Fact]
////    public void buildRateCourseViewModelTest_NullCourse()
////    {
////        Enrolled enrolled = _context.Enrolled.Find(9);
////        RateCourseViewModel rateCourseViewModel = _viewModelSerivce.buildRateCourseViewModel(enrolled, null);
////        Assert.Null(rateCourseViewModel);
////    }
////}