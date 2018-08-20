using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestDAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Edit(TEntity entity);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}