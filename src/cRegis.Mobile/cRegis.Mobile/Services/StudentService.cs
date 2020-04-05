using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cRegis.Mobile.Services
{
    public class StudentService : APIServiceBase, IStudentService
    {
        

        public StudentService(string jwt) : base(jwt)
        {

        }

        public async Task<Student> getStudentAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/student");

            var studentInfo = await response.Content.ReadAsStringAsync();

            Student s = JsonConvert.DeserializeObject<Student>(studentInfo);

            return s;
        }

        public async Task<string> getStudentCreditAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/student/credhrs");

            var crehrs = await response.Content.ReadAsStringAsync();

            return crehrs;
        }

        public async Task<List<Course>> getStudentCourseListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/course/taking");

            var courseInfo = await response.Content.ReadAsStringAsync();

            List<Course> courseList = JsonConvert.DeserializeObject<List<Course>>(courseInfo);

            return courseList;
        }

        public async Task<List<Enrolled>> getStudentEnrolledListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/enroll/current");

            var enrollInfo = await response.Content.ReadAsStringAsync();

            List<Enrolled> enrollList = JsonConvert.DeserializeObject<List<Enrolled>>(enrollInfo);

            return enrollList;
        }

        public async Task<Course> getCourseAsync(int cid)
        {
            var response = await _client.GetAsync(API_DOMAIN + String.Format("/course/{0}", cid));

            var CourseInfo = await response.Content.ReadAsStringAsync();

            Course c = JsonConvert.DeserializeObject<Course>(CourseInfo);

            return c;
        }

        public async Task<Faculty> getFaculty(int fid)
        {
            var response = await _client.GetAsync(API_DOMAIN + String.Format("/faculty/{0}", fid));

            var data = await response.Content.ReadAsStringAsync();

            Faculty f = JsonConvert.DeserializeObject<Faculty>(data);

            return f;
        }
    }
}
