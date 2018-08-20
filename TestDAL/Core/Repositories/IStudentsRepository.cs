using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using TestDAL.Core.Domains;

namespace TestDAL.Core.Repositories
{
  
    public interface IStudentsRepository  : IRepository<Student> 
    {


    }
}