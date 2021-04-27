using BusinessLogic.Abstractions;
using BusinessLogic.Implementations;
using BusinessLogic.Profiles;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddAutoMapper(typeof(UserProfile));
        }

        public static void AddBusinessLogicAuthentication(this IServiceCollection services)
        {
            services.AddDataAccessAuthentication();
        }
    }
}
