using Microsoft.Owin.Security;
using mvc_as_gateway_web.Api;
using mvc_as_gateway_web.Common;
using mvc_as_gateway_web.Models;
using mvc_as_gateway_web.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using mvc_as_gateway_web.ClaimsPrincipalExtensions;

namespace mvc_as_gateway_web.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        public IApiService _apiService { get; set; }

        public ManageController()
        {

        }

        public ActionResult Index()
        {
            var model = new IndexViewModel();

            model.Claims = ((ClaimsPrincipal)HttpContext.User).Claims.ToDictionary(k=> k.Type, v=> v.Value);

            return View(model);
        }
    }
}