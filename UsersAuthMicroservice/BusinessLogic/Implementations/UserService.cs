using AutoMapper;
using BusinessLogic.Abstractions;
using Microsoft.AspNetCore.Identity;
using Models.Users;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly IMapper mapper;

        public UserService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        async Task<UserGetAllDto> IUserService.Create(UserCreateDto newUser)
        {
            IdentityUser existingUser = await userManager.FindByEmailAsync(newUser.Email);


            if (existingUser != null)
            {
                return null;
            }

            IdentityUser user = mapper.Map<IdentityUser>(newUser);
            IdentityResult wasCreated = await userManager.CreateAsync(user, newUser.Password);

            if (!wasCreated.Succeeded)
            {
                return null;
            }

            return mapper.Map<UserGetAllDto>(user);
        }
    }
}
