﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prometheus.Web.Generics
{
    public class GenericHttpClient<T> : IGenericHttpClient<T> where T : class
    {
        #region "Private Members"
        private readonly string _baseAddress;
        private readonly string _token; //TODO: change type to token model
        #endregion

        #region "Constructors"
        public GenericHttpClient(string baseAddress)
        {
            _baseAddress = baseAddress;
        }
        public GenericHttpClient(string baseAddress, string token)
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
        private void SetupHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(_baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(_token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token); //.Add("Authorization:", string.Format("Bearer {0}", _token).ToString());
            }
        }

        /// <summary>
        /// Deletes the specified object. 
        /// </summary>
        /// <param name="apiUrl">Example: objectToDelete/1</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                SetupHttpClient(client);
                var response = await client.DeleteAsync(apiUrl).ConfigureAwait(false);
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<List<T>> GetMultipleItemsAsync(string apiUrl)
        {
            List<T> result = null;

            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                var response = await client.GetAsync(apiUrl).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                //if (response.IsSuccessStatusCode)
                //{
                //    result = JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());

                //}

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<List<T>>(x.Result);
                });
            }

            return result;
        }

        public async Task<T> GetSingleItemAsync(string apiUrl)
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

        public async Task<T> RefreshToken(string apiUrl, string bodyContent)
        {
            T result = null;

            using (var client = new HttpClient())
            {
                SetupHttpClient(client);

                HttpContent objectContent = new StringContent(bodyContent, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, objectContent);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });

                return result;
            }
        }
    }
}
