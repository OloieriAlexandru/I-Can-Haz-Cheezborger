using IdentityModel.Client;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
