using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Net.Generics
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext context;

        public GenericRepository(DbContext dataContext)
        {
            context = dataContext;
            DbSet = dataContext.Set<T>();
        }

        public T Insert(T entity)
        {
            return DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
