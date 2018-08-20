using TestDAL.Core.Domains;
using TestDAL.Core.Repositories;
using TestDAL.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace TestDAL.Persistence.Repositories
{
    public class StudentsRepository : Repository<Student>, IStudentsRepository
    {
        internal TestContext context;

        public StudentsRepository(DbContext context) : base(context) { }

        // public TestContext DatabaseContext => Context as TestContext;
    }
}