using cRegis.Core.Identities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cRegis.IntegrationTest.Infrastructure
{
    public class FakeSignInManager:SignInManager<StudentUser>
    {
        public FakeSignInManager(FakeUserManager userManager)
          : base(userManager,
          new Mock<IHttpContextAccessor>().Object,
          new Mock<IUserClaimsPrincipalFactory<StudentUser>>().Object,
          new Mock<IOptions<IdentityOptions>>().Object,
          new Mock<ILogger<SignInManager<StudentUser>>>().Object,
          new Mock<IAuthenticationSchemeProvider>().Object,
          new Mock<IUserConfirmation<StudentUser>>().Object)
        { }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            FakeSignInResult result = new FakeSignInResult();
            result.setSucceed();
            return Task.Run(() => { return (SignInResult)result; });
        }
    } 
}
