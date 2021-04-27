using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Auth;
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

        public AuthenticationRequest AuthenticationRequest { get; set; }
        public LoginUserModel LoginUserModel { get; set; }

        public LoginBase()
        {
            LoginUserModel = new LoginUserModel();
            AuthenticationRequest = new AuthenticationRequest();
        }

        protected async Task HandleValidSubmit()
        {
            await Task.CompletedTask;

            NavigationManager.NavigateTo("/index");
        }
    }
}
