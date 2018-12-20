using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using mvc_as_gateway.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace mvc_as_gateway.App_Start
{
    internal class AuthenticationAttribute : AuthorizeAttribute
    {

        public AuthenticationAttribute()
        {
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "authentication token is expired or not valid");
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var res = (JwtTokenHelper.ValidateToken(actionContext.Request.Headers.Authorization.Parameter));
                if (res != null)
                {
                    Thread.CurrentPrincipal = res;
                    HttpContext.Current.User = res;
                    //var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}