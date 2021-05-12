using Microsoft.AspNetCore.Components;
using Models.Trends;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class TrendsListBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }

        [Inject]
        public IAuthService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<TrendGetAllDto> Trends { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Trends = await TrendService.GetAll();
                StateHasChanged();
            }
        }

        protected void NavigateTrendId(string trendId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}", forceLoad: true);
        }

        protected void FollowTrend(TrendGetAllDto trend)
        {
            if (trend.Followed)
            {
                --trend.FollowersCount;
            }
            else
            {
                ++trend.FollowersCount;
            }
            trend.Followed = !trend.Followed;
        }
    }
}
