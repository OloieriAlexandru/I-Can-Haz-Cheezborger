using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Abstractions
{
    public interface IJwtService
    {
        public string GenerateJwtToken(IdentityUser user);
    }
}
