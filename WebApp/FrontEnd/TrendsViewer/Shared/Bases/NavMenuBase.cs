using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Shared.Bases
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ITrendService TrendService { get; set; }

        public IEnumerable<TrendGetAllDto> Trends { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Trends = await TrendService.GetPopular();
                StateHasChanged();
            }
        }
        protected void NavigateTrendId(Guid trendId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}", forceLoad: true);
        }
        protected void NavigateNewTrend()
        {
            NavigationManager.NavigateTo($"/trends/create", forceLoad: true);
        }
    }
}
