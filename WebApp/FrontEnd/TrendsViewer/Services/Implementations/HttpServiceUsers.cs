using System.Net.Http;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class HttpServiceUsers : HttpService
    {
        public HttpServiceUsers(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService)
        {
        }
    }
}
