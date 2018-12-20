using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvc_as_gateway;
using mvc_as_gateway.Controllers;
using mvc_as_gateway_web.Api;

namespace mvc_as_gateway.Tests.Controllers
{
    [TestClass]
    public class TodoControllerTest
    {
        [TestMethod]
        public void GetTodos()
        {
            // Arrange
            var client = new ApiService();
            string token = null;
            mvcasgateway.Api.Model.GetToDoResponse response = null;
            try { token = client.Login("admin@admin.com", "abcd1234"); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            // Act
            try { response = client.GetTodos("Bearer " + token); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            // Assert
            Assert.IsNotNull(token);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void CreateTodo()
        {
            // Arrange
            var client = new ApiService();
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            string token = null;
            bool inputValidationTestPassed = true;
            mvcasgateway.Api.Model.GetToDoResponse response = null;
            try { token = client.Login("admin@admin.com", "abcd1234"); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            // Act
            //username length exceed 50
            try { client.CreateTodo("Bearer " + token, "012345678901234567890123456789012345678901234567890123456789"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            //valid
            try { client.CreateTodo("Bearer " + token, guid); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; inputValidationTestPassed = false; }

            // Assert
            Assert.IsNotNull(token);
            Assert.IsTrue(inputValidationTestPassed);
        }
    }
}
