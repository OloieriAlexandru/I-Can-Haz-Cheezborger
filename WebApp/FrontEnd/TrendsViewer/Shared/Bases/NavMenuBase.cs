﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Shared.Bases
{
    public class NavMenuBase : ComponentBase
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

        protected async Task Logout()
        {
            await AuthService.Logout();
        }
    }
}
