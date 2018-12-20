using mvc_as_gateway_web.Api;
using mvc_as_gateway_web.ClaimsPrincipalExtensions;
using mvc_as_gateway_web.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mvc_as_gateway_web.Controllers
{
    [Authorize]
    public class TodoController : BaseController
    {
        public IApiService _apiService { get; set; }

        public TodoController()
        {

        }

        // GET: Todo
        public ActionResult Index()
        {
            var model = new IndexViewModel();

            try
            {
                model.TodoList = _apiService.GetTodos(User.Identity.GetToken()).Todo.Select(_ => new Todo
                {
                    Id = _.Id.GetValueOrDefault(),
                    Name = _.Name
                });
            }
            catch (Exception ex)
            {
                mvcasgateway.Api.Client.ApiException apiException = _apiService.TryCastApiException(ex);
                if (apiException != null)
                {
                    if ((HttpStatusCode)apiException.ErrorCode == HttpStatusCode.Unauthorized)
                        return RedirectToAction("Logout", "Account");
                    else if (!AddResponseErrorsToModelState(_apiService.GetErrorMessage(apiException)))
                    {
                        return View("Error");
                    }
                }
                else
                {
                    //TODO: Properly return Handle Error page
                    return View("Error");
                }
            }

            return View(model);
        }

        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _apiService.CreateTodo(User.Identity.GetToken(), model.Name);

                return RedirectToAction("Index");
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
    }
}