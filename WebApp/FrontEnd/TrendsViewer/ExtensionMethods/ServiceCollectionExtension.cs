using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrendsViewer.Services.Implementations;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Profiles;

namespace TrendsViewer.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ITrendService, TrendService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("TrendsMicroserviceApiUrl"));
            });

            services.AddHttpClient<IPostService, PostService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("TrendsMicroserviceApiUrl"));
            });

            services.AddHttpClient<ICommentService, CommentService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("TrendsMicroserviceApiUrl"));
            });

            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("UsersMicroserviceApiUrl"));
            });
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TrendProfile));
            services.AddAutoMapper(typeof(PostProfile));
        }
    }
}
