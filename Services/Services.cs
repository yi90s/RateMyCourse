using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.DomainModels;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels;
using cReg_WebApp.Models.ViewModels.CourseViewModels;
using cReg_WebApp.Models.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cReg_WebApp.Services
{
    public class Service
    {
        private readonly DataContext _context;


        public Service(DataContext context)
        {
            this._context = context;

        }


        public async Task<Student> findStudentById(int studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }

        public async void updateStudent(Student newStudent)
        {
            throw new NotImplementedException();
        }



        public async Task<Course> findCourseById(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<List<Models.entities.Course>> findCoursesByKeyWords(string keywords)
        {
            List<Models.entities.Course> result = _context.Courses.Where(c => c.courseName.Contains(keywords) || c.courseDescription.Contains(keywords)).ToList();

            return result;
        }

        internal bool verifyRegisterDetailForStudents(Student stu, int cid)
        {
            List<int> registeringCourse = _context.Enrolled.Where(e => stu.studentId == e.studentId && !e.completed).Select(e=> e.courseId).ToList();
            return (!registeringCourse.Contains(cid));
        }

        public async Task<List<Course>> findAllEligibleCoursesForStudent(Student student)
        {
            List<Course> allCourses = await _context.Courses.ToListAsync();
            List<Course> takingCourses = await findCurrentTakingCoursesForStudent(student);
            List<Course> takenCourses = await findAllTakenCoursesForStudent(student);
            allCourses.RemoveAll(c => takingCourses.Contains(c) && takenCourses.Contains(c));

            return allCourses;
        }

        public int findAvaliableSpaceForCourse(int cid)
        {
            return   _context.Courses.Find(cid).space - _context.Enrolled.Where(e => e.courseId == cid && !e.completed).Count();
        }
        internal CourseViewModel createCourseViewModel(int cid, Enrolled enroll = null)
        {
            Models.entities.Course thisCourse = _context.Courses.Find(cid);
            if (thisCourse != null)
            {
                var sIdAndComments = _context.Enrolled.Where(e => e.courseId == cid && e.completed && e.comment != null ).ToDictionary(e => e.studentId, e => e.comment);
                int count = 0;
                int totalRate = 0;
                Array rating = _context.Enrolled.Where(e => e.courseId == cid && e.completed && e.rating != null).Select(e => e.rating).ToArray();
                Dictionary<string,string> keyParis = new Dictionary<string, string>();
                foreach (KeyValuePair<int, string> sAndc in sIdAndComments)
                {
                    int sid = sAndc.Key;
                    Student stu = _context.Students.Find(sid);
                    keyParis.Add(stu.name, sAndc.Value);
                    count++;
                }
                foreach (int singleRate in rating)
                {
                    if (singleRate > 0 && singleRate < 100)
                    {
                        totalRate += singleRate;
                    }
                }
                string rate;
                if (count != 0)
                {
                    rate = (totalRate / count).ToString("0") + "/100";
                }
                else
                {
                    rate = "N/A";
                }

                int avaliableSpace = findAvaliableSpaceForCourse(thisCourse.courseId);

                if(enroll==null)
                {
                    return new CourseViewModel(rate, count, avaliableSpace, thisCourse, keyParis);
                }
                else
                {
                    return new CourseViewModel(enroll.enrollId,rate, count, avaliableSpace, thisCourse, keyParis);
                }
            }
            else
            {
                return null;
            }
        }

        internal bool verifyDropDetailForStudents(Student stu, int eid)
        {
            Enrolled thisEnroll = _context.Enrolled.Find(eid);
            return ( stu.studentId == thisEnroll.studentId && !thisEnroll.completed);
        }

        internal Task<WishListViewModel> createWishListViewModel(Student stu)
        {
            throw new NotImplementedException();
        }

        internal async Task<FindCourseViewModel> createFindCourseViewModel(Student student)
        {
            //if (student != null)
            //{
            //    int sid = student.studentId;
            //    List<int> takingCourseId = await _context.Enrolled.Where(e => (e.studentId == sid && !e.completed)).Select(e => e.courseId).ToListAsync().ConfigureAwait(false);
            //    List<Models.entities.Course> courseList = await _context.Courses.Where(c => !takingCourseId.Contains(c.courseId)).ToListAsync().ConfigureAwait(false);
            //    string majorName = _context.Faculties.Find(student.majorId).facultyName;
            //    FindCourseViewModel result = new FindCourseViewModel(student,majorName,courseList);
            //    return result;
            //}
            //else
            //{
            //    return null;
            //}

            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.RegisterCourse };
            List<Course> eligibleCourses = await findAllEligibleCoursesForStudent(student);

            foreach (Course c in eligibleCourses)
            {
                var ccvm = new CourseContainerViewModel(c, actions, student);
                ccvms.Add(ccvm);
            }

            FindCourseViewModel vmodel = new FindCourseViewModel(ccvms);

            return vmodel;


        }

        public async Task<CourseDetailViewModel> createCourseDetailViewModel(int cid)
        {
            Course c = await findCourseById(cid);
            CourseDetailViewModel vm;

            if(c == null)
            {
                return null;
            }

            List<Comment> comments = await findAllCommentsForCourse(c);

            if(comments.Count <= 0)
            {
                vm = new CourseDetailViewModel(c, null);
            }
            else
            {
                List<CourseCommentViewModel> commentsVM = new List<CourseCommentViewModel>();

                float? ratingSum = 0;
                foreach(var cmt in comments)
                {
                    ratingSum += cmt.ratingScore;
                    commentsVM.Add(new CourseCommentViewModel(cmt));
                }
                float? avgRating = ratingSum / comments.Count;

                vm = new CourseDetailViewModel(c, avgRating.ToString() + "/100", commentsVM);
            }

            return vm;

        }

        public async Task<List<Comment>> findAllCommentsForCourse(Course c)
        {
            
            if(c == null)
            {
                return null;
            }

            List<Comment> comments = new List<Comment>();
            List<Enrolled> enrolls = await _context.Enrolled.Where(e => e.courseId == c.courseId && e.completed).ToListAsync();
            foreach(var e in enrolls)
            {
                comments.Add(new Comment(e));
            }

            return comments;
        }

        public async Task<int> findRemainingCreditHourForStudent(Student student)
        {
            int creditHourNeed = _context.Faculties.Find(student.majorId).graduateCreditHours;
            int creditHourTook = 0;
            List<int> finshedCourseId = await _context.Enrolled.Where(e => e.studentId == student.studentId && e.completed).Select(e => e.courseId).ToListAsync();
            foreach (int courseId in finshedCourseId)
            {
                creditHourTook += _context.Courses.Find(courseId).creditHours;
            }
            return creditHourNeed - creditHourTook;
        }

        private async Task<List<Course>> getRecomendedCourseForStudents(Student student)
        { 
            Dictionary<int, int?> finishedCourseIdAndGrade = await _context.Enrolled.Where(e => e.studentId == student.studentId && e.completed).ToDictionaryAsync(e=>e.courseId,e=>e.grade) ;
            List<int> requiredCourseId = await _context.Required.Where(r => r.facultyId == student.majorId).Select(r => r.courseId).ToListAsync();
            List<int> completedAndTakingCourseId = await _context.Enrolled.Where(e => e.studentId == student.studentId).Select(e => e.courseId).ToListAsync();
            List<int> allCourseList = await _context.Courses.Where(c=> !completedAndTakingCourseId.Contains(c.courseId)).Select(c=>c.courseId).ToListAsync();
            List<Course> resultList = new List<Course>();
            List<Course> otherList = new List<Course>();
            int rightNowCreditHours = await findRemainingCreditHourForStudent(student);
            foreach(int courseId in allCourseList)
            {
                Dictionary<int, int> preRequisiteCourseIdAndGrade = await _context.Prerequisites.Where(p => p.courseId == courseId).ToDictionaryAsync(r => r.prerequisiteId, r => r.grade);
                bool completeAllPreRequisite = true;
                foreach (KeyValuePair<int, int> preIdAndGrade in preRequisiteCourseIdAndGrade)
                {
                    if (!finishedCourseIdAndGrade.ContainsKey(preIdAndGrade.Key))
                    {
                        completeAllPreRequisite = false;
                    }
                    else
                    {
                        int? grade = finishedCourseIdAndGrade[preIdAndGrade.Key];
                        if (grade < preIdAndGrade.Value)
                        {
                            completeAllPreRequisite = false;
                        }
                    }
                }
                if (completeAllPreRequisite && requiredCourseId.Contains(courseId))
                {
                    Course thisCourse = _context.Courses.Find(courseId);
                    rightNowCreditHours -= thisCourse.creditHours;
                    resultList.Add(thisCourse);
                }
                else if(completeAllPreRequisite)
                {
                    Course thisCourse = _context.Courses.Find(courseId);
                    otherList.Add(thisCourse);
                }
            }
            foreach(Course course in otherList)
            {
                if(rightNowCreditHours>0)
                {
                    rightNowCreditHours -= course.creditHours;
                    resultList.Add(course);
                }
            }
            return resultList;
        }

        internal async Task<ProfileViewModel> createProfileViewModel(Student student)
        {
            if(student == null)
            {
                return null;
            }
            int remainCreditHours = await findRemainingCreditHourForStudent(student);
            string majorName = (await _context.Faculties.FindAsync(student.majorId)).facultyName;
            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.ViewDetail, CourseActions.DropCourse };
            List<Course>  regCourses = await findCurrentTakingCoursesForStudent(student);

            foreach(Course c in regCourses)
            {
                var ccvm = new CourseContainerViewModel(c, actions);
                ccvms.Add(ccvm);
            }

            ProfileViewModel vmodel = new ProfileViewModel(student, ccvms, remainCreditHours);

            return vmodel;
           
        }

        public async Task<CourseContainerViewModel> createCourseContainerViewModel(Course c, ISet<CourseActions> actions, Enrolled e)
        {
            try
            {
                CourseContainerViewModel ccvm = new CourseContainerViewModel(c,actions, e);

                return ccvm;

            }catch(Exception)
            {
                return null;
            }
            
        }

        internal async Task<HistoryViewModel> createHistoryViewModel(Student student)
        {

            if (student == null)
            {
                return null;
            }

            List<CourseContainerViewModel> ccvms = new List<CourseContainerViewModel>();
            ISet<CourseActions> actions = new HashSet<CourseActions> { CourseActions.RateCourse, CourseActions.ViewDetail };
            List<Enrolled> takens = await findAllTakensForStudent(student);

            foreach (Enrolled enroll in takens)
            {
                Course course = await findCourseById(enroll.courseId);
                var ccvm = new CourseContainerViewModel(course, actions, enroll);
                ccvms.Add(ccvm);
            }

            HistoryViewModel vmodel = new HistoryViewModel(student, ccvms);

            return vmodel;
        }


        internal async Task<bool> verifyRegistrationForStudent(Student stu,int cid)
        {
            if(stu==null || findAvaliableSpaceForCourse(cid)<=0)
            {
                return false;
            }
            bool result = true;
            int sid = stu.studentId;
            List<Prerequisite> prerequisiteList = await _context.Prerequisites.Where(p => p.courseId == cid).ToListAsync().ConfigureAwait(false);
            foreach(Prerequisite require in prerequisiteList)
            {
                List<Enrolled> thisEnrolls = await _context.Enrolled.Where(e => e.studentId == sid && e.courseId == cid && e.completed).ToListAsync();
                if(thisEnrolls!=null)
                {
                    int grade = -1;
                    foreach(Enrolled enroll in thisEnrolls)
                    {
                        if(enroll.grade!=null && enroll.grade>grade)
                        {
                            grade = enroll.grade.GetValueOrDefault();
                        }
                    }
                    if(grade<require.grade)
                    {
                        result = false;
                    }
                }else
                {
                    result = false;
                }
            }
            return result;
        }

        internal async Task registerCourseForStudent(Student stu, int cid)
        {
            if(stu!=null)
            {
                Enrolled newEnroll = new Enrolled { courseId =  cid, studentId = stu.studentId, completed = false, grade = null, rating = null, comment = null };
                _context.Enrolled.Add(newEnroll);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<List<Course>> findAllRegisteredCoursesForStudent(Student student)
        {
            if (student == null)
                return null;
            List<Enrolled> enrolls = await findAllEnrollsForStudent(student);
            List<Course> enrolledCourses = new List<Course>();
            enrolls.ForEach(
                e => enrolledCourses.Add( _context.Courses.Find(e.courseId))
                );

            return enrolledCourses;
        }

        public async Task<List<Course>> findCurrentTakingCoursesForStudent(Student student)
        {
            if (student == null)
                return null;
            List<Enrolled> enrolls = (await findAllEnrollsForStudent(student)).Where(e => !e.completed).ToList();
            List<Course> enrolledCourses = new List<Course>();
            enrolls.ForEach(
                e => enrolledCourses.Add(_context.Courses.Find(e.courseId))
                );

            return enrolledCourses;
        }

        
        public async Task<List<Enrolled>> findAllRatingForCourse(Models.entities.Course course)
        {
            throw new NotImplementedException(); 
        }

        internal Task<List<Models.entities.Course>> findWishListCoursesForStudent(Student stu)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Enrolled>> findAllEnrollsForStudent(Student student)
        {
            return await _context.Enrolled.Where(e => e.studentId == student.studentId).ToListAsync();
        }

        public async Task<List<Course>> findAllTakenCoursesForStudent(Student student)
        {
            List<Enrolled> allRegs = await findAllEnrollsForStudent(student);
            List<Enrolled> takens = allRegs.Where(allRegs => allRegs.completed).ToList();
            List<int> takenCourseIds = takens.Select(taken => taken.courseId).ToList();
            List<Course> takenCourses = _context.Courses.Where(c => takenCourseIds.Contains(c.courseId)).ToList();

            return takenCourses;
            
        }

        public async Task<List<Enrolled>> findAllTakensForStudent(Student student)
        {
            List<Enrolled> allRegs = await findAllEnrollsForStudent(student);
            List<Enrolled> takens = allRegs.Where(allRegs => allRegs.completed).ToList();

            return takens;
        }

        internal async Task<bool> verifyDropForStudent(Student student, int eid)
        {
            Enrolled thisEnroll =  _context.Enrolled.Find(eid);

            bool result = (student.studentId == thisEnroll.studentId );

            return result;
        }

        internal async Task dropCourseForStudent(int eid)
        {
            Enrolled thisEnroll = _context.Enrolled.Find(eid);

            _context.Enrolled.Remove(thisEnroll);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        //this function only update an existing object
        public async void updateEnroll(Enrolled newEnroll)
        {
            if(newEnroll != null)
            {
                var change = _context.Enrolled.Update(newEnroll);
                if(change.State == EntityState.Modified)
                {
                    _context.SaveChanges();
                }
            }
        }

        internal CourseViewModel createCourseViewModel(int courseId, object p)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Enrolled>> findAllCurrentEnrollsForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrolled> findEnrollById(int enrollId)
        {
            return await _context.Enrolled.FindAsync(enrollId);
        }


        public async Task<Student> findCurrentStudent(ClaimsPrincipal user)
        {

            //try
            //{

            //    StudentUser curUser = await _userManager.GetUserAsync(user);
            //    return await findStudentById(curUser.StudentId);
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
            throw new NotImplementedException();
        }
            


    }
}
