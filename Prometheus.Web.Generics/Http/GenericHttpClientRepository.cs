using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Web.Generics.Http
{
    public class GenericHttpClientRepository<T> : IGenericHttpClientRepository<T> where T : class
    {
        #region "Private Members"
        private readonly string _baseAddress;
        private readonly string _token; //TODO: change type to token model
        #endregion

        #region "Constructors"
        public GenericHttpClientRepository(string baseAddress)
        {
            _baseAddress = baseAddress;
        }
        public GenericHttpClientRepository(string baseAddress, string token)
        {
            _baseAddress = baseAddress;
            _token = token;
        }
        #endregion

        /// <summary>
        /// Private member called at the beginning of each API method call. 
        /// Used to setup the base address, accept headers (application/json), and authentication headers for the request
        /// </summary>
        /// <param name="client">The HttpClient being configured</param>
        /// <param name="restMethod">GET, POST, PUT or DELETE. Aim to prevent hacker changing the 
        /// method from say GET to DELETE</param>
        /// <param name="apiUrl">The portion of the url we use to call the web Rest method</param>
        /// <param name="content">The object we are taling action on. Used for posts and puts</param>
        private void SetupHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(_token))
            {
                client.DefaultRequestHeaders.Add("Authorization:", string.Format("Bearer {0}", _token).ToString());
            }
        }
        public async Task<bool> DeleteAsync(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
        }

        public async Task<IEnumerable<T>> GetMultipleItemsAsync(string apiUrl)
        {
            IEnumerable<T> result = null;

            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<IEnumerable<T>>(x.Result);
                });
            }

            return result;
        }

        public async Task<T> GetBySingleItemAsync(string apiUrl)
        {
            T result = null;

            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        public async Task<T> PostAsync(string apiUrl, T entity)
        {
            T result = null;

            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var jsonIn = JsonConvert.SerializeObject(entity);
                HttpContent objectContent = new StringContent(jsonIn, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, objectContent);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        public Task<IQueryable<T[]>> SearchForAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PutAsync(string apiUrl, T entity)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var jsonIn = JsonConvert.SerializeObject(entity);
                HttpContent objectContent = new StringContent(jsonIn, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(apiUrl, objectContent);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
