using mvc_as_gateway_web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc_as_gateway_web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool AddResponseErrorsToModelState(string errorMessage)
        {
            try
            {
                var modelStateErrors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(errorMessage);

                if (modelStateErrors != null)
                {
                    foreach (var error in modelStateErrors)
                    {
                        foreach (var entry in
                            from entry in ModelState
                            let matchSuffix = string.Concat(".", entry.Key)
                            where error.Key.EndsWith(matchSuffix)
                            select entry)
                        {
                            ModelState.AddModelError(entry.Key, error.Value[0]);
                        }
                    }

                    return true;
                }
                else
                {
                    ModelState.AddModelError("", errorMessage);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}