using Microsoft.AspNetCore.Components;
using Models.Users;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class UserDetailsBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public UserGetByIdDto User { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                User = await UserService.GetById(Id);
                StateHasChanged();
            }
        }
    }
}
