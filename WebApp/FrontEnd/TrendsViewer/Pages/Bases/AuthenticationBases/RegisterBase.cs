using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Users;
using System.Threading.Tasks;
using TrendsViewer.Models;

namespace TrendsViewer.Pages.Bases.AuthenticationBases
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public RegisterUserModel RegisterUserModel { get; set; }

        public UserCreateDto User { get; set; }

        public RegisterBase()
        {
            RegisterUserModel = new RegisterUserModel();
            User = new UserCreateDto();
        }

        protected async Task HandleValidSubmit()
        {
            await Task.CompletedTask;

            Mapper.Map(RegisterUserModel, User);

            NavigationManager.NavigateTo("/");
        }
    }

}
