using Microsoft.AspNetCore.Components;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class UserComunityBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public IEnumerable<UserGetAllDto> Users { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Users = await UserService.GetAll();
                StateHasChanged();
            }
        }

        protected void GoToUserPage(Guid userId)
        {
            NavigationManager.NavigateTo("/users/" + userId);
        }
    }
}
