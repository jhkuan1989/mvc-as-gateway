using mvc_as_gateway.Common;
using mvc_as_gateway.DAL;
using mvc_as_gateway.DAL.Repositories;
using mvc_as_gateway.Models.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace mvc_as_gateway.Controllers
{
    public class AuthenticationController : ApiController
    {
        private IConnectionFactory connectionFactory;

        [HttpPost, AllowAnonymous, Route("auth")]
        public LoginResponse Login([FromBody] LoginRequest login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            connectionFactory = ConnectionHelper.GetConnection();
            var context = new DbContext(connectionFactory);

            var userRep = new UserRepository(context);
            var user = userRep.LoginUser(login.Username, login.Password);

            // if credentials are valid
            if (user != null)
            {
                string token = JwtTokenHelper.GenerateToken(user.UserId.ToString(), user.UserName, user.FirstName, user.LastName);

                //return the token
                return new LoginResponse
                {
                    Token = token
                };
            }
            else
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Incorrect Username or Password."));
            }
        }

        [HttpPost, AllowAnonymous, Route("register")]
        public IHttpActionResult Register([FromBody] RegisterRequest register)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            connectionFactory = ConnectionHelper.GetConnection();
            var context = new DbContext(connectionFactory);

            using (var uow = context.CreateUnitOfWork())
            {
                var userRep1 = new UserRepository(context);
                var userRep2 = new UserRepository(context);

                var user = userRep1.GetUserByUsernameOrEmail(string.Empty, register.Email);
                if (user == null)
                {
                    userRep2.CreateUser(new Domain.User
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        UserName = register.Email,
                        Email = register.Email,
                        Password = register.Password
                    });
                    uow.SaveChanges();
                }
                else
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, "email already exists in the system."));
            }

            return Ok();
        }
    }
}
