using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Web.Generics.Http
{
    public class GenericHttpClientRepository<T> : IGenericHttpClientRepository<T> where T : class
    {
        private readonly string _baseAddress;
        private readonly string _token; //TODO: change type to token model

        public Task<bool> DeleteAsync(string apiUrl)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T[]>> GetAllAsync(string apiUrl)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string apiUrl)
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertAsync(string apiUrl, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T[]>> SearchForAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string apiUrl, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
