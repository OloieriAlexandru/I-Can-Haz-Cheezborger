using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Service.Utils
{
    public class UserInfoExtractor
    {
        public static void Extract(ClaimsPrincipal claimsPrincipal, UserInfoModel model)
        {
            model.CreatorId = Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(
                c => c.Type == "Id").Value);
            model.CreatorUsername = claimsPrincipal.Claims.FirstOrDefault(
                c => c.Type == JwtRegisteredClaimNames.Sub).Value;
        }
    }
}
