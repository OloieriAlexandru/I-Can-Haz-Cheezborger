using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TrendsMicroservice.IntegrationTests.IntegrationTestsUtil
{
    public static class HttpRequestsMessagesFactory
    {
        private static string BEARER_TOKEN;

        static HttpRequestsMessagesFactory()
        {
            BEARER_TOKEN = ConfigurationProvider.GetConfiguration()["AUTH_TOKEN"];
        }

        public static HttpRequestMessage Get(HttpMethod method, string url, bool authenticate = true)
        {
            return Get(method, url, null, authenticate);
        }

        public static HttpRequestMessage Get(HttpMethod method, string url, object body, bool authenticate = true)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(method, url);
            if (body != null)
            {
                httpRequest.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            }
            if (authenticate)
            {
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BEARER_TOKEN);
            }
            return httpRequest;
        }
    }
}
