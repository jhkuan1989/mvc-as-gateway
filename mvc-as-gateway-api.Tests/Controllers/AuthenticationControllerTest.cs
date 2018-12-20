using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvc_as_gateway;
using mvc_as_gateway.Controllers;
using mvc_as_gateway_web.Api;

namespace mvc_as_gateway.Tests.Controllers
{
    [TestClass]
    public class AuthenticationControllerTest
    {
        [TestMethod]
        public void RegisterTest()
        {
            // Arrange
            var client = new ApiService();
            string guid = Guid.NewGuid().ToString().Replace("-","");
            bool inputValidationTestPassed = true;
            bool existingEmailValidationTestPassed = true;

            // Act
            //invalid email
            try { client.Register(guid, "test", "", "abcd1234", "abcd1234"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex;  }
            //invalid password length
            try { client.Register(guid + "@testing.com", "test", "", "abc", "abc"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex;  }
            //confirm password not same
            try { client.Register(guid + "@testing.com", "test", "", "abcd1234", "abcd12345"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex;  }

            //email length exceed 50
            try { client.Register(guid + "@testing12345678910.com", "test", "", "abcd1234", "abcd1234"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            //first name length exceed 25
            try { client.Register(guid + "@testing.com", "01234567890123456789123456", "", "abcd1234", "abcd1234"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            //last name length exceed 25
            try { client.Register(guid + "@testing.com", "", "01234567890123456789123456", "abcd1234", "abcd1234"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            //password length exceed 25
            try { client.Register(guid + "@testing.com", "test", "", "01234567890123456789123456", "01234567890123456789123456"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            
            //valid
            try { client.Register(guid + "@testing.com", "test", "", "abcd1234", "abcd1234"); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; inputValidationTestPassed = false; }
            //email exists
            try { client.Register(guid + "@testing.com", "test", "", "abcd1234", "abcd1234"); existingEmailValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            // Assert
            Assert.IsTrue(inputValidationTestPassed);
            Assert.IsTrue(existingEmailValidationTestPassed);
        }
        
        [TestMethod]
        public void LoginTest()
        {
            // Arrange
            var client = new ApiService();
            string guid = Guid.NewGuid().ToString().Replace("-", "");
            bool inputValidationTestPassed = true;
            string token = null;

            // Act
            //username length exceed 50
            try { client.Login(guid + "@testing12345678910.com", "abcd1234"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }
            //password length exceed 25
            try { client.Login("admin@admin.com", "01234567890123456789123456"); inputValidationTestPassed = false; } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            //valid
            try { token = client.Login("admin@admin.com", "abcd1234"); } catch (Exception ex) { mvcasgateway.Api.Client.ApiException apiException = (mvcasgateway.Api.Client.ApiException)ex; }

            // Assert
            Assert.IsTrue(inputValidationTestPassed);
            Assert.IsNotNull(token);
        }

    }
}
