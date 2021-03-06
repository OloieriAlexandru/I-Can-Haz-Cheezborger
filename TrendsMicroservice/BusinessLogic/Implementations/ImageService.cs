using BusinessLogic.Abstractions;
using BusinessLogic.Utils;
using Common.Utils;
using Microsoft.Extensions.Options;
using Models.Images;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;

namespace BusinessLogic.Implementations
{
    public class ImageService : IImageService
    {
        private readonly HttpClient httpClient;

        private readonly ImageServiceConfiguration imageServiceConfiguration;

        private readonly ServerConfiguration serverConfiguration;

        public ImageService(IHttpClientFactory httpClientFactory, IOptionsMonitor<ImageServiceConfiguration> optionsMonitor,
            IOptionsMonitor<ServerConfiguration> serverConfigurationServiceMonitor)
        {
            httpClient = httpClientFactory.CreateClient(Constants.IMAGES_MICROSERVICE_HTTP_CLIENT_NAME);
            imageServiceConfiguration = optionsMonitor.CurrentValue;
            serverConfiguration = serverConfigurationServiceMonitor.CurrentValue;
        }

        ImageGetDto IImageService.Create(ImageCreateDto imageCreateDto)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/v1/images")
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(imageCreateDto), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage responseMessage = httpClient.Send(httpRequestMessage);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return null;
            }
            using var reader = new StreamReader(responseMessage.Content.ReadAsStream());
            ImageGetDto imageGetDto = JsonConvert.DeserializeObject<ImageGetDto>(reader.ReadToEnd());
            return imageGetDto;
        }

        string IImageService.GetDefaultImageUrl()
        {
            return serverConfiguration.DefaultImageUrl;
        }

        string IImageService.GetFullImageUrl(string imagePath)
        {
            return imageServiceConfiguration.Url + '/' + imagePath;
        }
    }
}
