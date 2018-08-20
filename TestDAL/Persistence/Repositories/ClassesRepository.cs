using TestDAL.Core.Domains;
using TestDAL.Core.Repositories;
using TestDAL.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace TestDAL.Persistence.Repositories
{
    public class ClassesRepository : Repository<Class>, IClassesRepository
    {
        internal TestContext context;

        public ClassesRepository(DbContext context) : base(context) { }

        // public TestContext DatabaseContext => Context as TestContext;

    }
}