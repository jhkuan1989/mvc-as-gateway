using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Models.Todo
{
    public class PostTodoRequest
    {
        [StringLength(50, ErrorMessage = "Exceed Maximum Length of 50 characters.")]
        public string Name { get; set; }
    }
}