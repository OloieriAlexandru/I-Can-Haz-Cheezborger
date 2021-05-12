using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Users;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;
using Syncfusion.Blazor.Notifications;

namespace TrendsViewer.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUserService UserService { get; set; }

        public RegisterUserModel RegisterUserModel { get; set; }

        public RegisterBase()
        {
            RegisterUserModel = new RegisterUserModel();
        }

        public string ToastContentRegisterOK { get; set; } = "Enjoy our app";
        public SfToast ToastObjRegisterOK { get; set; }
        public string ToastContentRegisterFailed { get; set; } = "Check if your email is valid and passwords match";
        public SfToast ToastObjRegisterFailed { get; set; }

        protected async Task HandleValidSubmit()
        {
            UserCreateDto newUser = Mapper.Map<UserCreateDto>(RegisterUserModel);
            try
            {
                UserGetAllDto userGetAllDto = await UserService.Create(newUser);
                if (userGetAllDto != null)
                {
                    
                    NavigationManager.NavigateTo("/login");
                }
                else
                {
                    await ToastObjRegisterFailed.Show();
                }          
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}
