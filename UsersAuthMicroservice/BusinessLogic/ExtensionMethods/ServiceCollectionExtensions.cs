using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using BusinessLogic.Profiles;
using BusinessLogic.Utils;
using Common.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BusinessLogic.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessServices(connectionString);

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddAutoMapper(typeof(UserProfile));
        }

        public static void AddBusinessLogicAuthentication(this IServiceCollection services)
        {
            services.AddDataAccessAuthentication();
        }

        public static void AddBusinessLogicMicroservicesConfiguration(this IServiceCollection services, ImageServiceConfiguration imageServiceConfiguration)
        {
            services.AddHttpClient(Constants.IMAGES_MICROSERVICE_HTTP_CLIENT_NAME, client =>
            {
                client.BaseAddress = new Uri(imageServiceConfiguration.Url);
            });
        }
    }
}
