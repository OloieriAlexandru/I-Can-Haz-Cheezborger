using Models.Users;
using System;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Utils;

namespace TrendsViewer.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly MicroservicesUrls microservicesUrls;

        private readonly IUserService userService;

        public ImageService(IUserService userService, MicroservicesUrls microservicesUrls)
        {
            this.userService = userService;
            this.microservicesUrls = microservicesUrls;
        }

        public string GetFullUrl(string url)
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

        public string GetUserPhotoUrl(string url)
        {
            if (url == null)
            {
                return "https://cambodiaict.net/wp-content/uploads/2019/12/computer-icons-user-profile-google-account-photos-icon-account.jpg";
            }
            if (url.StartsWith("http"))
            {
                return url;
            }
            return microservicesUrls.ImagesMicroserviceApiUrl + url;
        }

        async Task<string> IImageService.GetUserPhotoUrl(Guid userId)
        {
            UserGetImageUrlDto getImageDto = await userService.GetImageUrl(userId);
            return GetUserPhotoUrl(getImageDto.ImageUrl);
        }
    }
}
