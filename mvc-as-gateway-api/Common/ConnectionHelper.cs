using mvc_as_gateway.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.Common
{
    public static class ConnectionHelper
    {
        public static IConnectionFactory GetConnection()
        {
            return new DbConnectionFactory("MyConString");
        }
    }
}