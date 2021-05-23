using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class NavBarBase : ComponentBase
    {

        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthService.Initialize();
                StateHasChanged();
            }
        }

        protected async Task Logout()
        {
            await AuthService.Logout();
            NavigationManager.NavigateTo("/", true);
        }

        protected void GoToUserProfile()
        {
            NavigationManager.NavigateTo("/users/" + AuthService.GetId());
        }

        protected void GoToOurMembers()
        {
            NavigationManager.NavigateTo("/members");
        }
    }
}
