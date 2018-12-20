using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.DAL
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}