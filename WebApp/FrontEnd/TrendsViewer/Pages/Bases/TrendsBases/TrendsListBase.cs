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
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<TrendGetAllDto> Trends { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Trends = await TrendService.GetTrends();
        }

        protected void NavigateTrendId(string trendId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}", forceLoad: true);
        }

        protected void FollowTrendId(TrendGetAllDto trend)
        {
            if (trend.FollowClicked)
            {
                trend.Followers--;
            }
            else
            {
                trend.Followers++;
            }
            trend.FollowClicked = !trend.FollowClicked;
        }
    }
}
