using GOH.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GOH.Data.Repositories
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public Repository()
        {
        }

        public Repository(TContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = Context.Set<TEntity>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }

            return set;
        }

        public IEnumerable<TEntity> GetWhere(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> set = Context.Set<TEntity>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
          
            return set.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
