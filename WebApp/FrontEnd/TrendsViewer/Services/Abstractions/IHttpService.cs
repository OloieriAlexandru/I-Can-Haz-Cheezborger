using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url);

        Task<T> Post<T>(string url, object value);
    }
}
