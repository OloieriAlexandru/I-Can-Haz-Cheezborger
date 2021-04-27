using Microsoft.AspNetCore.Components;
using Models.Users;
using System.Net.Http;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        async Task<UserGetAllDto> IUserService.Create(UserCreateDto newUser)
        {
            return await httpClient.PostJsonAsync<UserGetAllDto>("api/v1/users", newUser);
        }
    }
}
