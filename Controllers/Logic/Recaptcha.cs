using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
namespace cReg_WebApp.Controllers.Logic
{
    public class Recaptcha
    {
        string captchaResponse;

        public Recaptcha(string response)
        { 
            captchaResponse = response;
        }

        public bool IsReCaptchValid()
        {
            var result = false;
            var secretKey = "6Lf-EtUUAAAAAP5-bE08l018Y5LTCMfcWoI5pccY";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey, captchaResponse);
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    JObject jResponse = JObject.Parse(stream.ReadToEnd());
                    var isSuccess = jResponse.Value<bool>("success");
                    result = (isSuccess) ? true : false;
                }
            }

            return result;
        }
    }
}