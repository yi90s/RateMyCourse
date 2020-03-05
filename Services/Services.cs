using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly UserManager<StudentUser> _userManager;

        public Service(DataContext context, UserManager<StudentUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;

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

        public async Task<List<Course>> findCoursesByKeyWords(string keywords)
        {
            throw new NotImplementedException(); 
        }


        public async Task<List<Course>> findAllEligibleCoursesForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Course>> findAllCompletedCoursesForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        internal CourseViewModel createCourseViewModel(int cid, Enrolled enroll= null)
        {
            throw new NotImplementedException();
        }

        internal async Task<ProfileViewModel> createProfileViewModel(Student student)
        {
            if(student == null)
            {
                return null;
            }

            string majorName = (await _context.Faculties.FindAsync(student.majorId)).facultyName;
            var keyValues = new Dictionary<int, Course>();
            Dictionary<int, int> temp = _context.Enrolled.Where(e => e.studentId == student.studentId && !e.completed).ToDictionary(e => e.enrollId, e => e.courseId);
            foreach (KeyValuePair<int, int> pair in temp)
            {
                Course value = _context.Courses.Find(pair.Value);
                keyValues.Add(pair.Key, value);
            }

            
            ProfileViewModel vmodel = new ProfileViewModel(student, majorName, keyValues);

            return vmodel;
           
        }

        public async Task<List<Course>> findRecommendCoursesForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> verifyRegistrationForStudent(Student stu, Course addedCourse)
        {
            throw new NotImplementedException();
        }

        internal Task registerCourseForStudent(Student stu, Course addedCourse)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Course>> findAllRegisteredCoursesForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Course>> findCurrentTakingCoursesForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        
        public async Task<List<Enrolled>> findAllRatingForCourse(Course course)
        {
            throw new NotImplementedException(); 
        }

        internal Task<List<Course>> findWishListCoursesForStudent(Student stu)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Enrolled>> findAllEnrollsForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrolled> findTakenCourseForStudent(Student student, Course course)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> verifyDropForStudent(Enrolled thisEnroll)
        {
            throw new NotImplementedException();
        }

        internal Task dropCourseForStudent(Enrolled thisEnroll)
        {
            throw new NotImplementedException();
        }

        //this function only update an existing object
        public async void updateEnroll(Enrolled newEnroll)
        {
            if(newEnroll != null)
            {
                var change = _context.Enrolled.Update(newEnroll);
                if(change.State == EntityState.Modified)
                {
                    await _context.SaveChangesAsync().ConfigureAwait(false);
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

            try
            {

                StudentUser curUser = await _userManager.GetUserAsync(user);
                return await findStudentById(curUser.StudentId);
            }
            catch (Exception e)
            {
                return null;
            }
        }
            


    }
}
