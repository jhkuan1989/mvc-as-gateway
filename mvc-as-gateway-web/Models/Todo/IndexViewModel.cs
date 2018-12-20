using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway_web.Models.Todo
{
    public class IndexViewModel
    {
        public IEnumerable<Todo> TodoList { get; set; }
    }

    public class Todo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}