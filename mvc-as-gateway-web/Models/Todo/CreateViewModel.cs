using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_as_gateway_web.Models.Todo
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}