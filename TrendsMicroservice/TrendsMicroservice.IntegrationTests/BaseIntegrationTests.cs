using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace TrendsMicroservice.IntegrationTests
{
    public abstract class BaseIntegrationTests
    {
        protected readonly HttpClient TestHttpClient;
        
        protected BaseIntegrationTests()
        {
            var appFactory = new WebApplicationFactory<Service.Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(AppDbContext));
                        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("IntegrationTestsTrendsMicroserviceDb"));
                    });
                });
            TestHttpClient = appFactory.CreateClient();
        }
    }
}
