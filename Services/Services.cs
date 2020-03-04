using cReg_WebApp.Models;
using cReg_WebApp.Models.context;
using cReg_WebApp.Models.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            throw new NotImplementedException();
        }

        public async void updateStudent(Student newStudent)
        {
            throw new NotImplementedException();
        }







        public async Task<Course> findCourseById(int courseId)
        {
            throw new NotImplementedException();
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

        public async Task<List<Course>> findRecommendCoursesForStudent(Student student)
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


        public async Task<List<Enrolled>> findAllEnrollsForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrolled> findTakenCourseForStudent(Student student, Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrolled> updateEnroll(Enrolled newEnroll)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Enrolled>> findAllCurrentEnrollsForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrolled> findEnrollById(int enrollId)
        {
            throw new NotImplementedException();
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
