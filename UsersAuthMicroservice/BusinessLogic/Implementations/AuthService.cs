using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly IJwtService jwtService;

        public AuthService(UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
        }

        async Task<AuthenticationResponse> IAuthService.Authenticate(AuthenticationRequest authenticationRequest)
        {
            IdentityUser user = await userManager.FindByNameAsync(authenticationRequest.Email);
            
            if (user == null)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid credentials!"
                    }
                };
            }

            bool passwordIsCorrect = await userManager.CheckPasswordAsync(user, authenticationRequest.Password);

            if (!passwordIsCorrect)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid credentials!"
                    }
                };
            }

            return new AuthenticationResponse()
            {
                Success = true,
                Token = jwtService.GenerateJwtToken(user)
            };
        }
    }
}
