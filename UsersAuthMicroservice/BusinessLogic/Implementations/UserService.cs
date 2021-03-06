using AutoMapper;
using BusinessLogic.Abstractions;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.ImagesS;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly RoleManager<ModeratorRole> roleManager;

        private readonly IImageService imageService;

        private readonly IMapper mapper;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ModeratorRole> roleManager, IMapper mapper,
            IImageService imageService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        async Task<UserGetAllDto> IUserService.Create(UserCreateDto newUser)
        {
            ApplicationUser existingUser = await userManager.FindByEmailAsync(newUser.Email);

            if (existingUser != null)
            {
                return null;
            }

            ApplicationUser user = mapper.Map<ApplicationUser>(newUser);
            user.SecurityStamp = Guid.NewGuid().ToString();
            IdentityResult wasCreated = await userManager.CreateAsync(user, newUser.Password);

            if (!wasCreated.Succeeded)
            {
                return null;
            }

            return mapper.Map<UserGetAllDto>(user);
        }

        async Task<ICollection<UserGetAllDto>> IUserService.GetAll()
        {
            ICollection<ApplicationUser> users = await userManager.Users.ToListAsync();
            return mapper.Map<ICollection<UserGetAllDto>>(users);
        }

        async Task<UserGetByIdDto> IUserService.GetById(Guid id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id.ToString());
            UserGetByIdDto returnedUser = mapper.Map<UserGetByIdDto>(user);

            returnedUser.ModeratedTrendsIds = new List<Guid>();
            if (user.ModeratorRoles != null)
            {
                foreach (ModeratorUserRole userRole in user.ModeratorRoles)
                {
                    returnedUser.ModeratedTrendsIds.Add(Guid.Parse(userRole.Role.Name));
                }
            }

            return returnedUser;
        }

        async Task IUserService.PatchRole(UserPatchModeratorRoleDto patchRoleModel)
        {
            ModeratorRole role = await roleManager.FindByNameAsync(patchRoleModel.TrendId.ToString());

            if (role == null)
            {
                ModeratorRole newRole = new ModeratorRole()
                {
                    Name = patchRoleModel.TrendId.ToString()
                };
                IdentityResult result = await roleManager.CreateAsync(newRole);
                if (!result.Succeeded)
                {
                    return;
                }
            }

            ApplicationUser user = await userManager.FindByIdAsync(patchRoleModel.UserId.ToString());
            if (user == null)
            {
                return;
            }
            await userManager.AddToRoleAsync(user, patchRoleModel.TrendId.ToString());
        }

        async Task IUserService.DeleteRole(UserDeleteModeratorRoleDto deleteRoleModel)
        {
            ModeratorRole role = await roleManager.FindByNameAsync(deleteRoleModel.TrendId.ToString());

            if (role == null)
            {
                return;
            }

            ApplicationUser user = await userManager.FindByIdAsync(deleteRoleModel.UserId.ToString());
            if (user == null)
            {
                return;
            }
            await userManager.RemoveFromRoleAsync(user, deleteRoleModel.TrendId.ToString());
        }

        async Task IUserService.PatchUser(UserPatchDto patchDto)
        {
            ApplicationUser user = await userManager.FindByIdAsync(patchDto.Id.ToString());

            ImageGetDto image = imageService.Create(new ImageCreateDto()
            {
                Image = patchDto.Image,
                Prefix = "users"
            });

            if (image != null)
            {
                user.ImageUrl = image.Url;
                await userManager.UpdateAsync(user);
            }
        }

        async Task<UserGetImageUrlDto> IUserService.GetImageUrl(Guid id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return null;
            }

            return new UserGetImageUrlDto
            {
                ImageUrl = user.ImageUrl
            };
        }
    }
}
