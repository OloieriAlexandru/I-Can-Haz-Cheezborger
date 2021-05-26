using Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Resolvers;

namespace TrendsViewer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IHttpService httpService;

        private readonly ILocalStorageService localStorageService;

        private JwtSecurityToken jwt;

        public AuthService(HttpServiceResolver httpServiceResolver, ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
            httpService = httpServiceResolver("users");
        }

        public AuthenticationResponse AuthenticationResponse { get; private set; }

        AuthenticationResponse IAuthService.AuthenticationResponse => AuthenticationResponse;

        string IAuthService.GetClaim(string type)
        {
            return jwt.Claims.First(claim => claim.Type == type).Value;
        }

        string IAuthService.GetId()
        {
            return jwt.Claims.First(claim => claim.Type == "Id").Value;
        }

        string IAuthService.GetImageUrl()
        {
            Claim claim = jwt.Claims.FirstOrDefault(claim => claim.Type == "ImageUrl");
            if (claim != null)
            {
                return claim.Value;
            }
            return "https://cambodiaict.net/wp-content/uploads/2019/12/computer-icons-user-profile-google-account-photos-icon-account.jpg";
        }

        string IAuthService.GetUsername()
        {
            return jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        }

        async Task IAuthService.Initialize()
        {
            AuthenticationResponse = await localStorageService.GetItem<AuthenticationResponse>("token");
            UpdateTokenInfo();
        }

        bool IAuthService.IsAdmin()
        {
            Claim claim = jwt.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin");
            if (claim != null)
            {
                return bool.Parse(claim.Value);
            }
            return false;
        }

        bool IAuthService.IsLoggedIn()
        {
            return AuthenticationResponse != null;
        }

        async Task IAuthService.Login(AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse = await httpService.Post<AuthenticationResponse>("/api/v1/auth", authenticationRequest);
            await localStorageService.SetItem<AuthenticationResponse>("token", AuthenticationResponse);
            UpdateTokenInfo();
        }

        async Task IAuthService.Logout()
        {
            await localStorageService.RemoveItem("token");
            AuthenticationResponse = null;
        }

        private void UpdateTokenInfo()
        {
            if (AuthenticationResponse == null)
            {
                return;
            }
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            jwt = handler.ReadJwtToken(AuthenticationResponse.Token);
        }
    }
}
