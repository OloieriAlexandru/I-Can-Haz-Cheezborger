using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Users;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services.Abstractions;

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
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}
