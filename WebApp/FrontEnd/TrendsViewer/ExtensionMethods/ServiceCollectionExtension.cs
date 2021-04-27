using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrendsViewer.Services.Implementations;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Profiles;
using TrendsViewer.Services.Resolvers;
using System.Collections.Generic;

namespace TrendsViewer.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<HttpServiceTrends>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("TrendsMicroserviceApiUrl"));
            });
            
            services.AddHttpClient<HttpServiceUsers>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("UsersMicroserviceApiUrl"));
            });

            services.AddScoped<HttpServiceResolver>(serviceProvider => type =>
            {
                return type switch
                {
                    "trends" => serviceProvider.GetService<HttpServiceTrends>(),
                    "users" => serviceProvider.GetService<HttpServiceUsers>(),
                    _ => throw new KeyNotFoundException(string.Format("There is no HttpService of type %s", type)),
                };
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITrendService, TrendService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TrendProfile));
            services.AddAutoMapper(typeof(PostProfile));
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
