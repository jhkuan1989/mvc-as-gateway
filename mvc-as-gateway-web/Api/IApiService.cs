
using mvc_as_gateway_web.Models.Todo;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace mvc_as_gateway_web.Api
{
    public interface IApiService
    {
        #region Authentication
        void Register(string email, string firstname, string lastname, string password, string confirmPassword);

        string Login(string username, string password);
        #endregion

        #region Todo
        mvcasgateway.Api.Model.GetToDoResponse GetTodos(string token);

        void CreateTodo(string token, string name);
        #endregion

        mvcasgateway.Api.Client.ApiException TryCastApiException(Exception ex);

        string GetErrorMessage(mvcasgateway.Api.Client.ApiException ex);
    }
}
