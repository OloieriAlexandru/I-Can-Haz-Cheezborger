using Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetAllDto>> GetAll();
        
        Task<UserGetByIdDto> GetById(string userid);

        Task<UserGetAllDto> Create(UserCreateDto newUser);

        Task<ValueTask> Patch(UserPatchDto patchedUser);

        Task<UserGetImageUrlDto> GetImageUrl(Guid id);
    }
}
