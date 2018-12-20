using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Models.Todo
{
    public class GetToDoResponse
    {
        public IEnumerable<Domain.Todo> Todo { get; set; }
    }
}