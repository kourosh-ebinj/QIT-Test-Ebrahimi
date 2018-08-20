using TestDAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestDAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentsRepository Students { get; }
        IClassesRepository Classes { get; }

        int Commit();
    }
}
