using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TestDAL.Core.Domains;
using TestDAL.Core.Repositories;

namespace TestDAL.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
        

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Edit(TEntity entity)
        {
            entity.ModifiedAt = DateTime.Now;
            //Context.Entry(entity).State = EntityState.Modified;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(int id)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(c => c.Id == id);
            this.Remove(entity);
        }
        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }
    }
}