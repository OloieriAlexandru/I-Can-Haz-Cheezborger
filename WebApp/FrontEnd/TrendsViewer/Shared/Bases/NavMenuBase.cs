using Microsoft.AspNetCore.Components;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Shared.Bases
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
    
        public NavMenuBase()
        {
        }
    }
}
