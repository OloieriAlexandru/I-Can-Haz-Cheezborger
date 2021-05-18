using DataAccess.Abstractions;
using DataAccess.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IFileRepository, FileRepository>();

            services.AddScoped<IImageInfoRepository, ImageInfoRepository>();
        }
    }
}
