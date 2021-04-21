﻿using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
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


        public UserLoginDto User { get; set; }
        public LoginUserModel LoginUserModel { get; set; }

        public LoginBase()
        {
            LoginUserModel = new LoginUserModel();
            User = new UserLoginDto();
        }

        protected async Task HandleValidSubmit()
        {
            //Mapper.Map(LoginUserModel, User);

           // await PostService.CreatePost(Guid.Parse(Id), Post);
            NavigationManager.NavigateTo("/index");
        }
    }
}
