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
    public class CourseService : APIServiceBase, ICourseService
    {
        

        public CourseService(string jwt) : base(jwt)
        {

        }

        public async Task<List<Course>> getCourseListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/course/eligible");

            var courseInfo = await response.Content.ReadAsStringAsync();

            List<Course> courseList = JsonConvert.DeserializeObject<List<Course>>(courseInfo);

            return courseList;
        }

        public async Task<List<Course>> getHistoryListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/course/completed");

            var courseInfo = await response.Content.ReadAsStringAsync();

            List<Course> courseList = JsonConvert.DeserializeObject<List<Course>>(courseInfo);

            return courseList;
        }

        public async Task<List<Comment>> getCourseCommentAsync(int cid)
        {
            var response = await _client.GetAsync(API_DOMAIN + String.Format("/course/{0}/comments", cid));

            var courseInfo = await response.Content.ReadAsStringAsync();

            List<Comment> commentList = JsonConvert.DeserializeObject<List<Comment>>(courseInfo);

            return commentList;
        }
    }
}
