using Entities;

namespace BusinessLogic.Abstractions
{
    public interface IJwtService
    {
        public string GenerateJwtToken(ApplicationUser user);
    }
}
