using Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<UserGetAllDto> Create(UserCreateDto newUser);

        Task<ICollection<UserGetAllDto>> GetAll();

        Task<UserGetByIdDto> GetById(Guid id);

        Task PatchRole(UserPatchModeratorRoleDto patchRoleModel);

        Task PatchUser(UserPatchDto patchDto);

        Task<UserGetImageUrlDto> GetImageUrl(Guid id);

        Task DeleteRole(UserDeleteModeratorRoleDto deleteRoleModel);
    }
}
