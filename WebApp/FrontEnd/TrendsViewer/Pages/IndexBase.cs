using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using Syncfusion.Blazor.Notifications;

namespace TrendsViewer.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        public string ToastContentLoggedIn { get; set; } = "Authentication succeded";
        public string ToastContentLoggedOut { get; set; }
        public SfToast ToastObjLoggedIn { get; set; }
        public SfToast ToastObjLoggedOut { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthService.Initialize();
                StateHasChanged();
                if (AuthService.IsLoggedIn())
                {
                    ToastObjLoggedIn.Show();
                }
                else
                {
                    ToastObjLoggedOut.Show();
                }
                
            }
        }
    }
}
