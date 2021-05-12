using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthService.Initialize();
                StateHasChanged();
            }
        }
    }
}
