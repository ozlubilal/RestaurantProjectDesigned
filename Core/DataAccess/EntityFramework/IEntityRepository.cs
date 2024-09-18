using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public interface IEntityRepository<T> where T : class, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> include = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
