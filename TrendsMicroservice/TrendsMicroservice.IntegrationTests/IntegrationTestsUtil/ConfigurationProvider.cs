using Microsoft.Extensions.Configuration;

namespace TrendsMicroservice.IntegrationTests.IntegrationTestsUtil
{
    public static class ConfigurationProvider
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
        }
    }
}
