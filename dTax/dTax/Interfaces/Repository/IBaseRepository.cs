using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace dTax.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> Select(Expression<Func<T, bool>> expression);
        IEnumerable<T> Select(Expression<Func<T, bool>> expression, IQueryable<T> query);

        IEnumerable<T> GetList();
        T Insert(T entity);

        void Update(T entity);
        void Delete(T entity);

        int Count();
        int Count(Expression<Func<T, bool>> expression);

        void Commit();
    }
}
