using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace mvc_as_gateway_web.ClaimsPrincipalExtensions 
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSubId(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("userid");
            return claim?.Value;
        }

        public static string GetUsername(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("username");
            return claim?.Value;
        }

        public static string GetFirstName(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("firstname");
            return claim?.Value;
        }

        public static string GetLastName(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("lastname");
            return claim?.Value;
        }

        public static string GetExpiredIn(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("exp");
            return claim?.Value;
        }

        public static string GetToken(this IIdentity principal)
        {
            var claim = ((ClaimsIdentity)principal).FindFirst("token");
            return claim?.Value;
        }
    }


}