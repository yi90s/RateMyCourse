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
            var response = await _client.GetAsync(API_DOMAIN + "/student/crehrs");

            var crehrs = await response.Content.ReadAsStringAsync();
            //int result = JsonConvert.DeserializeObject<int>(crehrs);

            return crehrs;
        }

        public async Task<List<Course>> getStudentCourseListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/course/taking");

            var courseInfo = await response.Content.ReadAsStringAsync();

            List<Course> courseList = JsonConvert.DeserializeObject<List<Course>>(courseInfo);

            return courseList;
        }
    }
}
