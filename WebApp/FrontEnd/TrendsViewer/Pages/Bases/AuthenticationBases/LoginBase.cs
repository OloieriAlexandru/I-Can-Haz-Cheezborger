using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Auth;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;
using Syncfusion.Blazor.Notifications;


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

        public string ToastContentLoggingStatus { get; set; } = "Wrong password or unregistered account";
        public SfToast ToastObjLoggingStatus { get; set; }
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
                else
                {
                    ToastObjLoggingStatus.Show();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // TODO: Display error
        }
    }
}
