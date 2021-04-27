using Models.Users;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IUserService
    {
        Task<UserGetAllDto> Create(UserCreateDto newUser); 
    }
}
