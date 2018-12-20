using mvc_as_gateway_web.Models.Todo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using mvcasgatewayConfig = mvcasgateway.Api.Client.Configuration;

namespace mvc_as_gateway_web.Api
{
    public class ApiService : IApiService
    {
        #region Authentication
        public void Register(string email, string firstname, string lastname, string password, string confirmPassword)
        {
            var client = new mvcasgatewayAPIService("");

            var response = client.AuthenticationApi.AuthenticationRegisterWithHttpInfo(new mvcasgateway.Api.Model.RegisterRequest
            {
                Email = email,
                Password = password,
                FirstName = firstname,
                LastName = lastname,
                ConfirmPassword = confirmPassword
            });
        }

        public string Login(string username, string password)
        {
            var client = new mvcasgatewayAPIService("");

            var response = client.AuthenticationApi.AuthenticationLoginWithHttpInfo(new mvcasgateway.Api.Model.LoginRequest
            {
                Username = username,
                Password = password,
            });

            return response.Data.Token;
        }
        #endregion

        #region Todo
        public mvcasgateway.Api.Model.GetToDoResponse GetTodos(string token)
        {
            var client = new mvcasgatewayAPIService(token);

            var response = client.TodoApi.TodoGetWithHttpInfo();

            return response.Data;             
        }

        public void CreateTodo(string token, string name)
        {
            var client = new mvcasgatewayAPIService(token);

            var response = client.TodoApi.TodoPostWithHttpInfo(new mvcasgateway.Api.Model.PostTodoRequest
            {
                Name = name,
            });
        }
        #endregion

        public mvcasgateway.Api.Client.ApiException TryCastApiException(Exception ex)
        {
            try
            {
                return (mvcasgateway.Api.Client.ApiException)ex;
            }
            catch
            {
                return null;
            }
        }

        public string GetErrorMessage(mvcasgateway.Api.Client.ApiException ex)
        {
            var errorCon = ex.ErrorContent;
            var jsontoken = JObject.Parse(errorCon);

            if (jsontoken["ModelState"] != null)
                return jsontoken["ModelState"].ToString();
            else if (jsontoken["Message"] != null)
                return jsontoken["Message"].ToString();
            else
                return Convert.ToString(errorCon);
        }
    }

    public class mvcasgatewayAPIService
    {
        private string AccessToken;

        public mvcasgatewayAPIService(string token)
        {
            AccessToken = token;
        }

        private mvcasgatewayConfig mvcasgatewayConfig;

        private mvcasgatewayConfig GetmvcasgatewayConfiguration()
        {
            if (mvcasgatewayConfig == null)
            {
                mvcasgatewayConfig = new mvcasgatewayConfig()
                {
                    DefaultHeader = new Dictionary<string, string>() { { "authorization", AccessToken } }
                };
            }
            mvcasgatewayConfig.Default.ApiClient.RestClient.BaseUrl = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
            return mvcasgatewayConfig;
        }

        internal mvcasgateway.Api.Api.AuthenticationApi AuthenticationApi { get { return new mvcasgateway.Api.Api.AuthenticationApi(GetmvcasgatewayConfiguration()); } }

        internal mvcasgateway.Api.Api.TodoApi TodoApi { get { return new mvcasgateway.Api.Api.TodoApi(GetmvcasgatewayConfiguration()); } }
    }

}