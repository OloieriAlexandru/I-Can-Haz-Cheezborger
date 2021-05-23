using Common.Utils;
using DataAccess.Abstractions;
using DataAccess.Implementations;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataAccess.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IImageInfoRepository, ImageInfoRepository>();
        }

        public static void AddCloudServicesDb(this IServiceCollection services, GoogleCloudConfig googleCloudConfig, MongoDbConfig mongoDbConfig)
        {
            services.AddScoped(s => new MongoClient(mongoDbConfig.ConnectionString));

            StorageClientBuilder storageClientBuilder = new StorageClientBuilder
            {
                CredentialsPath = googleCloudConfig.KeyPath
            };

            services.AddScoped(s => storageClientBuilder.Build());
        }
    }
}
