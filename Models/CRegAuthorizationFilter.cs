using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Models
{
    public class CRegAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {

                int studentId = Int32.Parse(context.HttpContext.Request.Query["studentId"]);
                int password = Int32.Parse(context.HttpContext.Request.Query["password"]);
                
            }
            catch(Exception e)
            {

            }
        }

    }
}
