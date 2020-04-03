using cRegis.Core.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cRegis.Tests.IntegrationTest.Infrastructure
{
    public class FakeUserManager : UserManager<StudentUser>
    {
        public FakeUserManager(DbContext context )
            : base(new UserStore<StudentUser>(context),
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<StudentUser>>().Object,
                  new IUserValidator<StudentUser>[0],
                  new IPasswordValidator<StudentUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<StudentUser>>>().Object)
        { }

        public override Task<StudentUser> GetUserAsync(ClaimsPrincipal principal)
        {
            foreach(StudentUser stu in Users)
            {
                if(stu.UserName.Equals(principal.Identity.Name))
                {
                    return Task.Run(() => { return stu; });
                }
            }
            return null;
        }
    }
}
