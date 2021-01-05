using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeExpressions);
        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        TEntity Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeExpressions);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);        
    }
}
