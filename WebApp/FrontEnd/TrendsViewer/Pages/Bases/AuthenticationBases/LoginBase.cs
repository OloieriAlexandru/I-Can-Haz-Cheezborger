using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Auth;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }

        public LoginUserModel LoginUserModel { get; set; }

        public LoginBase()
        {
            LoginUserModel = new LoginUserModel();
        }

        protected async Task HandleValidSubmit()
        {
            AuthenticationRequest authenticationRequest = Mapper.Map<AuthenticationRequest>(LoginUserModel);
            try
            {
                await AuthService.Login(authenticationRequest);

                if (AuthService.IsLoggedIn())
                {
                    NavigationManager.NavigateTo("/");
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // TODO: Display error
        }
    }
}
