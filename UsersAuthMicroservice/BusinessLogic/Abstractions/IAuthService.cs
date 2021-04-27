using Models.Auth;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);
    }
}
