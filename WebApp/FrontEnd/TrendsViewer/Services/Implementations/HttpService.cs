using Models.Auth;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public abstract class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        private readonly ILocalStorageService localStorageService;

        protected HttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        async Task<T> IHttpService.Delete<T>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
            return await SendRequest<T>(request);
        }

        async Task<T> IHttpService.Get<T>(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            return await SendRequest<T>(request);
        }

        async Task<T> IHttpService.Patch<T>(string url, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
            };
            return await SendRequest<T>(request);
        }

        async Task<T> IHttpService.Post<T>(string url, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
            };
            return await SendRequest<T>(request);
        }

        async Task<T> IHttpService.Put<T>(string url, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
            };
            return await SendRequest<T>(request);
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            AuthenticationResponse tokenResponse = await localStorageService.GetItem<AuthenticationResponse>("token");
            bool isApiUrl = !request.RequestUri.IsAbsoluteUri;

            if (tokenResponse != null && isApiUrl)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
            }

            using HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
