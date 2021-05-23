using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using Common.Utils;
using DataAccess.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BusinessLogic.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddDataAccessServices();

            services.AddScoped<IImageBusinessLogic, ImageBusinessLogic>();
        }

        public static void AddCloudServices(this IServiceCollection services, GoogleCloudConfig googleCloudConfig, MongoDbConfig mongoDbConfig)
        {
            Environment.SetEnvironmentVariable(googleCloudConfig.KeyEnvironmentVariableName, googleCloudConfig.KeyPath);

            services.AddCloudServicesDb(googleCloudConfig, mongoDbConfig);
        }
    }
}
