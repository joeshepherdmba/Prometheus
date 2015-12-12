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
        Task<T> PostAsync(string apiUrl, T entity);
        Task<bool> DeleteAsync(string apiUrl);
        Task<List<T>> GetMultipleItemsAsync(string apiUrl);
        Task<T> GetBySingleItemAsync(string apiUrl);
        Task<bool> PutAsync(string apiUrl, T entity);
    }
}
