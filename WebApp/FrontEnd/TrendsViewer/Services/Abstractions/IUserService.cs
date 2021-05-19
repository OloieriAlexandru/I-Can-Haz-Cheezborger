using Models.Trends;
using Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserGetAllDto>> GetAll();
        Task<UserGetByIdDto> GetById(string userid);
        Task<UserGetAllDto> Create(UserCreateDto newUser); 
    }
}
