using BusinessLogic.ExtensionMethods;
using Common.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCloudServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleTasksConfiguration>(configuration.GetSection("GoogleTasksConfiguration"));
            services.Configure<ImageServiceConfiguration>(configuration.GetSection("ImageServiceConfiguration"));
            services.Configure<ServerConfiguration>(configuration.GetSection("ServerConfiguration"));

            services.AddGCloudServices(configuration.GetSection("GoogleTasksConfiguration").Get<GoogleTasksConfiguration>(),
                configuration.GetSection("ImageServiceConfiguration").Get<ImageServiceConfiguration>());
        }
    }
}
