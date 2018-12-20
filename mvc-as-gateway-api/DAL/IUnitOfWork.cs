using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_as_gateway.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose();

        void SaveChanges();
    }
}