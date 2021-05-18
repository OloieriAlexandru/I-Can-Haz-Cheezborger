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
            services.Configure<GoogleCloudConfig>(configuration.GetSection("GoogleCloudConfig"));

            services.AddGCloudServices(configuration.GetSection("GoogleCloudConfig").Get<GoogleCloudConfig>());
        }
    }
}
