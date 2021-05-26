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

        bool IsAdmin();

        string GetUsername();

        string GetId();

        string GetImageUrl();

        string GetClaim(string type);
    }
}
