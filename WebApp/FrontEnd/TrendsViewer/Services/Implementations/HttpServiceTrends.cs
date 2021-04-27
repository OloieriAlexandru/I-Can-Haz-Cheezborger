using System.Net.Http;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class HttpServiceTrends : HttpService
    {
        public HttpServiceTrends(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService)
        {
        }
    }
}
