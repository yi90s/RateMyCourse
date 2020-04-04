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
    public class ModifyService : APIServiceBase, IModifyService
    {
        

        public ModifyService(string jwt) : base(jwt)
        {

        }

        public async Task<string> registerCourseAsync(int cid)
        {
            var response = await _client.PostAsync(API_DOMAIN + String.Format("/student/register/{0}", cid), null);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> dropCourseAsync(int eid)
        {
            var response = await _client.DeleteAsync(API_DOMAIN + String.Format("/enroll/{0}", eid));

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
