using dTax.Data.Interfaces;
using dTax.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        DbPostrgreContext context;

        public BaseRepository(DbPostrgreContext dbContext)
        {
            context = dbContext;
        }

        public void Commit()
        {
            context.SaveChanges(true);
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return expression == null ? context.Set<T>().Count() : context.Set<T>().Where(expression).Count();
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> expression, IQueryable<T> query)
        {
            return expression == null ? query.Where<T>(_ => true) : query.Where<T>(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().First(expression);
        }



        public T Insert(T entity)
        {
            EntityEntry<T> entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Added;
            return entityEntry.Entity;
        }

        public IQueryable<T> GetQuery()
        {
            return context.Set<T>();
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }



        public IEnumerable<T> GetList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> expression)
        {
            return expression == null ? context.Set<T>() : context.Set<T>().Where(expression);
        }


    }
}
