using Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Resolvers;

namespace TrendsViewer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IHttpService httpService;

        public UserService(HttpServiceResolver httpServiceResolver)
        {
            httpService = httpServiceResolver("users");
        }

        async Task<UserGetAllDto> IUserService.Create(UserCreateDto newUser)
        {
            return await httpService.Post<UserGetAllDto>("api/v1/users", newUser);
        }

        async Task<IEnumerable<UserGetAllDto>> IUserService.GetAll()
        {
            string url = "api/v1/users";
            return await httpService.Get<UserGetAllDto[]>(url);
        }

        async Task<UserGetByIdDto> IUserService.GetById(string userid)
        {
            string url = "api/v1/users/" + userid;
            return await httpService.Get<UserGetByIdDto>(url);
        }
    }
}
