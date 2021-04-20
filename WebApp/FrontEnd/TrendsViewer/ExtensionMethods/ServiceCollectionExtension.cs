using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrendsViewer.Services.Implementations;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Profiles;
using TrendsViewer.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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
        }

        public static void AddDependencyInjectionMappings(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TrendProfile));
            services.AddAutoMapper(typeof(PostProfile));
        }

        public static void AddIdentityServerConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            // Taken from: https://github.com/kevinrjones/SettingUpIdentityServer/blob/master/recordeddemo/client/WeatherMvc/Startup.cs
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;

                options.Authority = configuration["InteractiveServiceSettings:AuthorityUrl"];
                options.ClientId = configuration["InteractiveServiceSettings:ClientId"];
                options.ClientSecret = configuration["InteractiveServiceSettings:ClientSecret"];

                options.ResponseType = "code";
                options.UsePkce = true;
                options.ResponseMode = "query";

                options.Scope.Add(configuration["InteractiveServiceSettings:Scopes:0"]);
                options.SaveTokens = true;
            });

            services.Configure<IdentityServerSettings>(configuration.GetSection("IdentityServerSettings"));
        }
    }
}
