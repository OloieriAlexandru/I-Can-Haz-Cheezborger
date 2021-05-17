using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace TrendsViewer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    string port = Environment.GetEnvironmentVariable("PORT");
                    if (port == null)
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>()
                            .ConfigureKestrel(options =>
                            {
                                options.Listen(IPAddress.Any, Convert.ToInt32(port));
                            });
                    }
                });
    }
}
