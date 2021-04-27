using Models.Auth;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IAuthService
    {
        AuthenticationResponse AuthenticationResponse { get; }

        Task Initialize();

        Task Login(AuthenticationRequest authenticationRequest);

        Task Logout();

        bool IsLoggedIn();
    }
}
