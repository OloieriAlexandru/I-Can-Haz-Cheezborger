using BusinessLogic.Abstractions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Implementations
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ConfigurationDbContext configurationDbContext;

        private readonly PersistedGrantDbContext persistedGrantDbContext;

        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<IdentityUser> userManager;

        public DatabaseSeeder(ConfigurationDbContext _configurationDbContext, PersistedGrantDbContext _persistedGrantDbContext,
            ApplicationDbContext _applicationDbContext, UserManager<IdentityUser> _userManager)
        {
            configurationDbContext = _configurationDbContext;
            persistedGrantDbContext = _persistedGrantDbContext;
            applicationDbContext = _applicationDbContext;
            userManager = _userManager;
        }

        void IDatabaseSeeder.Seed()
        {
            InitPersistedGrantDbContext();

            SeedConfigurationDbContext();

            SeedApplicationDbContext();
        }

        private void InitPersistedGrantDbContext()
        {
            persistedGrantDbContext.Database.Migrate();
        }

        private void SeedConfigurationDbContext()
        {
            configurationDbContext.Database.Migrate();

            configurationDbContext.Clients.RemoveRange(configurationDbContext.Clients);
            foreach (var client in Data.SeedData.Clients)
            {
                configurationDbContext.Clients.Add(client.ToEntity());
            }
            configurationDbContext.SaveChanges();

            configurationDbContext.IdentityResources.RemoveRange(configurationDbContext.IdentityResources);
            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in Data.SeedData.IdentityResources)
                {
                    configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }

            configurationDbContext.ApiResources.RemoveRange(configurationDbContext.ApiResources);
            if (!configurationDbContext.ApiResources.Any())
            {
                foreach (var resource in Data.SeedData.ApiResources)
                {
                    configurationDbContext.Add(resource.ToEntity());
                }
                configurationDbContext.SaveChanges();
            }
        }

        private void SeedApplicationDbContext()
        {
            applicationDbContext.Database.Migrate();

            var admin = userManager.FindByNameAsync("admin").Result;
            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true
                };
                var result = userManager.CreateAsync(admin, "Password123$").Result;
                if (!result.Succeeded)
                {
                    throw new System.Exception(result.Errors.First().Description);
                }
            }
        }
    }
}
