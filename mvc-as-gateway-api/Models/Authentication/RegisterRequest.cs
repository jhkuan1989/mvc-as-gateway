using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Models.Authentication
{
    public class RegisterRequest
    {

        [EmailAddress]
        [StringLength(50, ErrorMessage = "Exceed Maximum Length of 50 characters.")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "Exceed Maximum Length of 25 characters..")]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "Exceed Maximum Length of 25 characters.")]
        public string LastName { get; set; }


        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}