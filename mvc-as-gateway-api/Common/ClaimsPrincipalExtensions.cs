using System.Security.Claims;
using System.Security.Principal;

namespace System.Web.Http
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetSubId(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("userid");
            return claim?.Value;
        }

        public static string GetUsername(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("username");
            return claim?.Value;
        }

        public static string GetFirstName(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("firstname");
            return claim?.Value;
        }

        public static string GetLastName(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("lastname");
            return claim?.Value;
        }

        public static string GetExpiredIn(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("exp");
            return claim?.Value;
        }

        public static string GetToken(this IPrincipal principal)
        {
            var claim = (principal as ClaimsPrincipal).FindFirst("token");
            return claim?.Value;
        }
    }
}