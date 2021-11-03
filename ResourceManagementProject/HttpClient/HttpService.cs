using Microsoft.Extensions.Configuration;
using ResourceManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ResourceManagementProject.HttpClient
{
    public class HttpService
    {
        private System.Net.Http.HttpClient _httpClient;

        public System.Net.Http.HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }

        public HttpService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string url = "https://localhost:44301/";
        public IConfiguration Configuration { get; }
        public HttpService()
        {
            //ConnectionConfigure connectionConfigure = new ConnectionConfigure(new ConnectionString(Configuration.GetConnectionString("ConnectionString")).Value);
            //new ConnectionString(Configuration.GetConnectionString("ConnectionString")).Value
            _httpClient = new System.Net.Http.HttpClient();
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public async Task<T> GetRequest<T>(string actionUrl, int? Id = null, string multiFormURIQueryString = "")
        {
            T result = default(T);

            if (Id.HasValue)
                actionUrl = string.Format(actionUrl + "?Id={0}", Id);
            else if (!string.IsNullOrWhiteSpace(multiFormURIQueryString))
                actionUrl = actionUrl + "?" + multiFormURIQueryString;

            HttpResponseMessage response = await HttpClient.GetAsync(actionUrl);

            if (response.IsSuccessStatusCode)
                result = response.Content.ReadAsAsync<T>().Result;
            else
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public T GetRequestSync<T>(string actionUrl, int? Id = null, string multiFormURIQueryString = "")
        {
            T result = default(T);

            if (Id.HasValue)
                actionUrl = string.Format(actionUrl + "?Id={0}", Id);
            else if (!string.IsNullOrWhiteSpace(multiFormURIQueryString))
                actionUrl = actionUrl + "?" + multiFormURIQueryString;

            HttpResponseMessage response = HttpClient.GetAsync(actionUrl).Result;

            if (response.IsSuccessStatusCode)
                result = response.Content.ReadAsAsync<T>().Result;/*need to add Microsoft.AspNet.WebApi.Client */
            else
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public async Task<U> SendRequest<T, U>(string actionUrl, T viewModel)
        {
            U result = default(U);

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(actionUrl, viewModel);

            if (response.IsSuccessStatusCode)
                result = response.Content.ReadAsAsync<U>().Result;
            else
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        public U SendRequestSync<T, U>(string actionUrl, T viewModel)
        {
            U result = default(U);

            HttpResponseMessage response = HttpClient.PostAsJsonAsync(actionUrl, viewModel).Result;

            if (response.IsSuccessStatusCode)
                result = response.Content.ReadAsAsync<U>().Result;
            else
                throw new Exception(response.Content.ReadAsStringAsync().Result);

            return result;
        }
    }
}
