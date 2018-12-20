using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Models.Authentication
{
    public class LoginResponse
    {
        /// <summary>
        /// Password
        /// </summary>
        public string Token { get; set; }
    }
}