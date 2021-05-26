using Microsoft.AspNetCore.Components;
using Models.Trends;
using System.Threading.Tasks;

namespace TrendsViewer.Pages
{
    public class TrendCardBase : ComponentBase
    {
        [Parameter]
        public TrendGetAllDto Trend { get; set; }

        [Parameter]
        public string TrendName { get; set; }

        [Parameter]
        public string TrendDescription { get; set; }

        [Parameter]
        public bool InList { get; set; } = true;

        [Parameter]
        public string ImageUrl { get; set; }

        [Parameter]
        public bool ActivateButtons { get; set; }

        [Parameter]
        public bool Authenticated { get; set; }

        [Parameter]
        public EventCallback<TrendGetAllDto> OnTrendFollowed { get; set; }

        [Parameter]
        public EventCallback<TrendGetAllDto> OnNavigateToTrendPage { get; set; }

        protected async Task FollowTrend()
        {
            await OnTrendFollowed.InvokeAsync(Trend);
        }

        protected async Task NavigateToTrendPage()
        {
            await OnNavigateToTrendPage.InvokeAsync(Trend);
        }
    }
}
