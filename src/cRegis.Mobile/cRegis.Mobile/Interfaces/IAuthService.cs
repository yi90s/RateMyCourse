using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.Mobile.Interfaces
{
    public interface IAuthService
    {
        //return a base64-encoded jwt token
        Task<HttpResponseMessage> jwtAuthenticate(string userName, string password);

    }
}
