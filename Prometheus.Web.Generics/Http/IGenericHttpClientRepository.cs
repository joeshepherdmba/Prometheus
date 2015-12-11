using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Web.Generics.Http
{
    public interface IGenericHttpClientRepository<T>
    {
        Task<T> InsertAsync(string apiUrl, T entity);
        Task<bool> DeleteAsync(string apiUrl);
        Task<IQueryable<T[]>> SearchForAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T[]>> GetAllAsync(string apiUrl);
        Task<T> GetByIdAsync(string apiUrl);
        Task<bool> UpdateAsync(string apiUrl, T entity);
    }
}
