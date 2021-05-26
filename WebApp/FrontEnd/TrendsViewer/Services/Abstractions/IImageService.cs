using System;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IImageService
    {
        string GetFullUrl(string url);

        string GetUserPhotoUrl(string url);

        Task<string> GetUserPhotoUrl(Guid userId);
    }
}
