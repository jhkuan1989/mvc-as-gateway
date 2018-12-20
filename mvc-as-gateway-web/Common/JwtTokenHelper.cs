using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace mvc_as_gateway_web.Common
{
    public class JwtTokenHelper
    {
        const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        public static IPrincipal ValidateToken(string token)
        {
            try
            {
                var now = DateTime.UtcNow;
                var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));

                SecurityToken securityToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = "MvcWebClient",
                    ValidIssuer = "AuthAPI",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = LifetimeValidator,
                    IssuerSigningKey = securityKey
                };
                //extract and assign the user of the jwt
                var simplePrinciple = handler.ValidateToken(token, validationParameters, out securityToken);
                if (simplePrinciple == null)
                    return null;

                var identity = simplePrinciple.Identity as ClaimsIdentity;

                if (identity == null)
                    return null;

                if (!identity.IsAuthenticated)
                    return null;

                identity.AddClaim(new Claim("token", token));

                return new ClaimsPrincipal(identity);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }  
}