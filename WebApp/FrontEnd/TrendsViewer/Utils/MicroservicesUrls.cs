namespace TrendsViewer.Utils
{
    public class MicroservicesUrls
    {
        public string TrendsMicroserviceApiUrl { get; set; }

        public string UsersMicroserviceApiUrl { get; set; }

        public string ImagesMicroserviceApiUrl { get; set; }

        public MicroservicesUrls(string trendsMicroserviceApiUrl, string usersMicroserviceApiUrl, string imagesMicroserviceApiUrl)
        {
            this.TrendsMicroserviceApiUrl = trendsMicroserviceApiUrl;
            this.UsersMicroserviceApiUrl = usersMicroserviceApiUrl;
            this.ImagesMicroserviceApiUrl = imagesMicroserviceApiUrl;
        }
    }
}
