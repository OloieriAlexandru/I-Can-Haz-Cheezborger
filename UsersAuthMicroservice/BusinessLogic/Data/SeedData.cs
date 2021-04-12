using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Data
{
    public static class SeedData
    {
        public static readonly IEnumerable<IdentityResource> IdentityResources = CreateEnumerable<IdentityResource>(
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            });

        public static readonly IEnumerable<ApiScope> ApiScopes = CreateEnumerable<ApiScope>(
            new ApiScope[]
            {
                new ApiScope("trendapi.read"),
                new ApiScope("trendapi.write")
            });

        public static readonly IEnumerable<ApiResource> ApiResources = CreateEnumerable<ApiResource>(
            new ApiResource[]
            {
                new ApiResource("trendapi")
                {
                    Scopes = new List<string> { "trendapi.read", "trendapi.write" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            });

        public static readonly IEnumerable<Client> Clients = CreateEnumerable<Client>(
            new Client[]
            {
                new Client
                {
                    ClientId = "trends-microservice",
                    ClientName = "Trends Microservice",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("SuperSecretPassword".Sha256()) },

                    AllowedScopes = { "trendapi.read", "trendapi.write" }
                }
            });

        private static IEnumerable<T> CreateEnumerable<T>(T[] items)
        {
            return items ?? Enumerable.Empty<T>();
        }
    }
}
