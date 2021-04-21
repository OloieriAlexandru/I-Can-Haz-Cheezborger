using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System.Threading.Tasks;
using TrendsViewer.Models;

namespace TrendsViewer.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UserLoginDto User { get; set; }
        public LoginUserModel LoginUserModel { get; set; }

        public LoginBase()
        {
            LoginUserModel = new LoginUserModel();
            User = new UserLoginDto();
        }

        protected async Task HandleValidSubmit()
        {
            await Task.CompletedTask;

            NavigationManager.NavigateTo("/index");
        }
    }
}
