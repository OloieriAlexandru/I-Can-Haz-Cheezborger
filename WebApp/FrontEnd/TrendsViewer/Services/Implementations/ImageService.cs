using TrendsViewer.Services.Abstractions;
using TrendsViewer.Utils;

namespace TrendsViewer.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly MicroservicesUrls microservicesUrls;

        public ImageService(MicroservicesUrls microservicesUrls)
        {
            this.microservicesUrls = microservicesUrls;
        }

        string IImageService.GetFullUrl(string url)
        {
            if (url == null)
            {
                return "";
            }
            if (url.StartsWith("http"))
            {
                return url;
            }
            return microservicesUrls.ImagesMicroserviceApiUrl + url;
        }
    }
}
