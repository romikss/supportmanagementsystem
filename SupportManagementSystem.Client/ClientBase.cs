using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SupportManagementSystem.Client
{
    public abstract class ClientBase : IDisposable
    {
        private readonly Lazy<HttpClient> _httpClient;
        private readonly Uri _uri;

        public ClientBase(Uri uri)
        {
            _uri = uri;

            _httpClient = new Lazy<HttpClient>(() => GetHttpClient());
        }

        ~ClientBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private HttpClient Client => _httpClient.Value;

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Client != null)
                {
                    Client.Dispose();
                }
            }
        }

        protected async Task<TEntity> GetEntityAsync<TEntity>(string path)
        {
            TEntity entity = default(TEntity);
            var response = await Client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<TEntity>(responseString);
            }

            return entity;
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = _uri;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
