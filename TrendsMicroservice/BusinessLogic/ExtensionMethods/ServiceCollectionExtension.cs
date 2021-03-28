using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using DataAccess.ExtensionMethods;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLogicServices(this IServiceCollection services, string connectionString)
        {
            services.AddDataAccessServices(connectionString);

            services.AddScoped<ITrendBusinessLogic, TrendBusinessLogic>();
        }
    }
}
