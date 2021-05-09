using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrendsViewer.ExtensionMethods;
using Syncfusion.Blazor;

namespace TrendsViewer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddAutoMapperProfiles();
            services.AddHttpClients(Configuration);
            services.AddSyncfusionBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDQzNDQ0QDMxMzkyZTMxMmUzMFNObW9NaEhlMmg5dm5sUVVHcUFvM2RlclFXUUxhOTBhM01kU1crM2ZzMzg9");
        }
    }
}
