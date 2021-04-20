using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Utils;

namespace TrendsViewer.Services.Implementations
{
    // Taken from: https://github.com/kevinrjones/SettingUpIdentityServer/blob/master/recordeddemo/client/WeatherMvc/Services/TokenService.cs
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> logger;
        private readonly IOptions<IdentityServerSettings> identityServerSettings;
        private readonly DiscoveryDocumentResponse discoveryDocument;

        public TokenService(ILogger<TokenService> logger, IOptions<IdentityServerSettings> identityServerSettings)
        {
            this.logger = logger;
            this.identityServerSettings = identityServerSettings;

            using HttpClient httpClient = new HttpClient();
            discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl).Result;
            if (discoveryDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is: {discoveryDocument.Error}");
                throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
            }
        }

        async Task<TokenResponse> ITokenService.GetToken(string scope)
        {
            using HttpClient httpClient = new HttpClient();
            TokenResponse tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = identityServerSettings.Value.ClientName,
                ClientSecret = identityServerSettings.Value.ClientPassword,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
