using Models.Auth;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private IHttpService httpService;

        private ILocalStorageService localStorageService;

        public AuthService(IHttpService httpService, ILocalStorageService localStorageService)
        {
            this.httpService = httpService;
            this.localStorageService = localStorageService;
        }

        public AuthenticationResponse AuthenticationResponse { get; private set; }

        async Task IAuthService.Initialize()
        {
            AuthenticationResponse = await localStorageService.GetItem<AuthenticationResponse>("token");
        }

        bool IAuthService.IsLoggedIn()
        {
            return AuthenticationResponse != null;
        }

        async Task IAuthService.Login(AuthenticationRequest authenticationRequest)
        {
            AuthenticationResponse = await httpService.Post<AuthenticationResponse>("", authenticationRequest);
            await localStorageService.SetItem<AuthenticationResponse>("token", AuthenticationResponse);
        }

        async Task IAuthService.Logout()
        {
            await localStorageService.RemoveItem("token");
            AuthenticationResponse = null;
        }
    }
}
