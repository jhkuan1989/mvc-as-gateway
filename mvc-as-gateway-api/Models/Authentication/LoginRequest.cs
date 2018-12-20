using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Models.Authentication
{
    public class LoginRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        [StringLength(50, ErrorMessage = "Exceed Maximum Length of 50 characters.")]
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}