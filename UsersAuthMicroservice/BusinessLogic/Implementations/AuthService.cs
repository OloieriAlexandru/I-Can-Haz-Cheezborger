using BusinessLogic.Abstractions;
using Entities;
using Microsoft.AspNetCore.Identity;
using Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IJwtService jwtService;

        public AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
        }

        async Task<AuthenticationResponse> IAuthService.Authenticate(AuthenticationRequest authenticationRequest)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(authenticationRequest.Email);
            
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
