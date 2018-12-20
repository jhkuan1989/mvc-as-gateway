using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.DAL.Extensions
{
    public static class IDbCommandExtensions
    {
        public static IDbDataParameter CreateParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }
    }
}