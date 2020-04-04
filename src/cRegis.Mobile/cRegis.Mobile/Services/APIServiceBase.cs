using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace cRegis.Mobile.Services
{
    public class APIServiceBase
    {
        protected HttpClient _client;
        protected static string API_DOMAIN = "http://ec2-15-223-82-164.ca-central-1.compute.amazonaws.com";
        public APIServiceBase(string jwt)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt);
        }

        public APIServiceBase()
        {
            _client = new HttpClient();
        }
    }
}
