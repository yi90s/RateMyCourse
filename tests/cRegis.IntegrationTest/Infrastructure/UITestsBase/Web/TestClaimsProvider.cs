using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace cRegis.Tests.IntegrationTest.Infrastructure
{
    public class TestClaimsProvider
    {
        public IList<Claim> Claims { get; }

        public TestClaimsProvider(IList<Claim> claims)
        {
            Claims = claims;
        }

        public TestClaimsProvider()
        {
            Claims = new List<Claim>();
        }

        public static TestClaimsProvider WithStudentClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            provider.Claims.Add(new Claim(ClaimTypes.Name, "jb"));
            provider.Claims.Add(new Claim(ClaimTypes.Role, "Student"));

            return provider;
        }
    }
}
