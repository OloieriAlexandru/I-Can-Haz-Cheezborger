using BusinessLogic.Abstractions;
using Common.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BusinessLogic.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig jwtConfig;

        public JwtService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            jwtConfig = optionsMonitor.CurrentValue;
        }

        string IJwtService.GenerateJwtToken(IdentityUser user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
