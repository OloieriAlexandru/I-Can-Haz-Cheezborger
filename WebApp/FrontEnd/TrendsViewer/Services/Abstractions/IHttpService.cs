using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url);

        Task<T> Post<T>(string url, object value);

        Task<T> Put<T>(string url, object value);

        Task<T> Delete<T>(string url);

        Task<T> Patch<T>(string url, object value);
    }
}
