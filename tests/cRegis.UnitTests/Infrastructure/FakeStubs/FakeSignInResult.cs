using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace cRegis.Tests.Infrastructure.FakeStubs
{
    public class FakeSignInResult:SignInResult
    {
        public FakeSignInResult():base(){}

        public void setSucceed()
        {
            base.Succeeded = true;
        }
    }
}
