using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Web.Generics
{
    public interface IGenericHttpClient<T>
    {
        Task<T> PostAsync(string apiUrl, T entity);
        Task<bool> DeleteAsync(string apiUrl);
        Task<List<T>> GetMultipleItemsAsync(string apiUrl);
        Task<T> GetSingleItemAsync(string apiUrl);
        Task<bool> PutAsync(string apiUrl, T entity);
        Task<T> RefreshToken(string apiUrl, string bodyContent);
    }
}
