using cRegis.Mobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Mobile.Services
{
    public class AuthService : APIServiceBase, IAuthService
    {
        public AuthService()
        {

        }

        public async Task<HttpResponseMessage> jwtAuthenticate(string userName, string password)
        {
            string authHeader64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", userName, password)));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader64);
            var stringContent = new StringContent("");
            HttpResponseMessage response = await _client.PostAsync(API_DOMAIN + "/auth", stringContent);

            return response;
        }
    }
}
