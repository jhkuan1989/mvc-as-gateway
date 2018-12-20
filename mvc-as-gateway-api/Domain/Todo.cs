using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Domain
{
    public class Todo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UpdatedBy { get; set; }
    }
}