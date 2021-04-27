using Models.Users;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<UserGetAllDto> Create(UserCreateDto newUser);
    }
}
