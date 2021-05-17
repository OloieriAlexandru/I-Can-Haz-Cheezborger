using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Service.Utils
{
    public static class UserInfoExtractor
    {
        public static void Extract(ClaimsPrincipal claimsPrincipal, UserInfoModel model)
        {
            model.UserId = Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(
                c => c.Type == "Id").Value);
            model.Username = claimsPrincipal.Claims.FirstOrDefault(
                c => c.Type == JwtRegisteredClaimNames.Sub).Value;
        }
    }
}
