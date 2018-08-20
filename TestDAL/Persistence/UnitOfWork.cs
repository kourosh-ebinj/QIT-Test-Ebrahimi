using TestDAL.Core;
using TestDAL.Core.Repositories;
using TestDAL.Persistence.Contexts;
using TestDAL.Persistence.Repositories;

namespace TestDAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestContext _context;
        private IStudentsRepository _Students;
        private IClassesRepository _Classes;

        public UnitOfWork(TestContext context)
        {
            _context = context;
            _Students = new StudentsRepository(_context);
            _Classes = new ClassesRepository(_context);
        }

        public IStudentsRepository Students { get { return _Students; } }
        public IClassesRepository Classes { get { return _Classes; } }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}