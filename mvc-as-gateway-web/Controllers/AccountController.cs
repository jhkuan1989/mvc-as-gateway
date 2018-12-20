using Microsoft.Owin.Security;
using mvc_as_gateway_web.Api;
using mvc_as_gateway_web.Common;
using mvc_as_gateway_web.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace mvc_as_gateway_web.Controllers
{
    public class AccountController : BaseController
    {
        public IApiService _apiService { get; set; }

        public AccountController()
        {

        }


        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _apiService.Register(model.Email, model.FirstName, model.LastName, model.Password, model.ConfirmPassword);

                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                mvcasgateway.Api.Client.ApiException apiException = _apiService.TryCastApiException(ex);
                if (apiException != null)
                {
                    if ((HttpStatusCode)apiException.ErrorCode == HttpStatusCode.Unauthorized)
                        return RedirectToAction("Logout");
                    else if (!AddResponseErrorsToModelState(_apiService.GetErrorMessage(apiException)))
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                string token = _apiService.Login(model.Email, model.Password);
                var principal = JwtTokenHelper.ValidateToken(token) as ClaimsPrincipal;

                int expInSec = 0;
                int.TryParse(principal.Claims.FirstOrDefault(_ => _.Type.Equals("exp"))?.Value, out expInSec);

                AuthenticationProperties options = new AuthenticationProperties();
                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expInSec);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim("userid", principal.Claims.FirstOrDefault(_ => _.Type.Equals("userid"))?.Value),
                    new Claim("username", principal.Claims.FirstOrDefault(_ => _.Type.Equals("username"))?.Value),
                    new Claim("firstname", principal.Claims.FirstOrDefault(_ => _.Type.Equals("firstname"))?.Value),
                    new Claim("lastname", principal.Claims.FirstOrDefault(_ => _.Type.Equals("lastname"))?.Value),
                    new Claim("exp", principal.Claims.FirstOrDefault(_ => _.Type.Equals("exp"))?.Value),
                    new Claim("exputc", options.ExpiresUtc.ToString() ),
                    new Claim("token", string.Format("Bearer {0}", token)),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                Request.GetOwinContext().Authentication.SignIn(options, identity);

                return RedirectToAction("Index", "Manage");
            }
            catch (Exception ex)
            {
                mvcasgateway.Api.Client.ApiException apiException = _apiService.TryCastApiException(ex);
                if (apiException != null)
                {
                    if ((HttpStatusCode)apiException.ErrorCode == HttpStatusCode.Unauthorized)
                        return RedirectToAction("Logout");
                    else if (!AddResponseErrorsToModelState(_apiService.GetErrorMessage(apiException)))
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }
}