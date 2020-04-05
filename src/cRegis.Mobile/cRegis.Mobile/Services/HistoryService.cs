using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;

namespace cRegis.Mobile.Services
{
    public class HistoryService : APIServiceBase, IHistoryService
    {
        

        public HistoryService(string jwt) : base(jwt)
        {

        }

        public async Task<List<Enrolled>> getHistoryEnrolledListAsync()
        {
            var response = await _client.GetAsync(API_DOMAIN + "/enroll/completed");

            var enrollInfo = await response.Content.ReadAsStringAsync();

            List<Enrolled> enrollList = JsonConvert.DeserializeObject<List<Enrolled>>(enrollInfo);

            return enrollList;
        }

        public async Task<string> postCommentAsync(Enrolled e)
        {
            var data = JsonConvert.SerializeObject(e);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(API_DOMAIN + "/enroll", content);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

    }
}
