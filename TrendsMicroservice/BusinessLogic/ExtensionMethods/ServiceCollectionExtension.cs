using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using BusinessLogic.Profiles;
using BusinessLogic.Utils;
using Common.Utils;
using DataAccess.ExtensionMethods;
using Google.Cloud.Tasks.V2;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BusinessLogic.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLogicServices(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessServices(connectionString);

            services.AddScoped<ITrendBusinessLogic, TrendBusinessLogic>();

            services.AddScoped<IPostBusinessLogic, PostBusinessLogic>();

            services.AddScoped<ICommentBusinessLogic, CommentBusinessLogic>();

            services.AddScoped<IContentScanTaskService, GCloudContentScanTaskService>();

            services.AddScoped<IImageService, ImageService>();

            services.AddAutoMapper(typeof(TrendProfile), typeof(PostProfile), typeof(CommentProfile));
        }

        public static void AddGCloudServices(this IServiceCollection services, GoogleTasksConfiguration configuration,
            ImageServiceConfiguration imageServiceConfiguration)
        {
            Environment.SetEnvironmentVariable(configuration.KeyEnvironmentVariableName, configuration.KeyPath);

            CloudTasksClientBuilder cloudTasksClientBuilder = new CloudTasksClientBuilder
            {
                CredentialsPath = configuration.KeyPath
            };

            services.AddHttpClient(Constants.IMAGES_MICROSERVICE_HTTP_CLIENT_NAME, client =>
            {
                client.BaseAddress = new Uri(imageServiceConfiguration.Url);
            });
            services.AddScoped(s => cloudTasksClientBuilder.Build());
        }
    }
}
