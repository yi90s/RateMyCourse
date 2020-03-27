using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cRegis.Core.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using cRegis.API.Models;
using Microsoft.Extensions.Options;
using cRegis.API.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace cRegis.API.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<StudentUser> _userManager;
        private readonly SignInManager<StudentUser> _signInManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<StudentUser> signInManager,
            UserManager<StudentUser> userManager,
            IOptions<AppSettings> appSettings
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [Route("[controller]")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Index([FromHeader] string authorization)
        {
            string authInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Split(" ")[1]));
            string userName = authInfo.Split(":")[0];
            string password = authInfo.Split(":")[1];
            var authModel = new AuthenticateModel { Username = userName, Password = password };

            var user = await _userManager.FindByNameAsync(authModel.Username);
            var result = await _signInManager.CheckPasswordSignInAsync(user, authModel.Password, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return getToken(user);
            }
            else
            {
                return StatusCode(401);
            }

        }


        private string getToken(StudentUser user) 
        { 
            if(user == null)
            {
                throw new ArgumentNullException("Cannot Generate Token on a null user");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id),
                    new Claim("sid", user.StudentId.ToString()),
                    new Claim(ClaimTypes.Role, "Student")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return token;
        }


    }
}