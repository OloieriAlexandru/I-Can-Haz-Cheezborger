using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Pages
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ITrendService TrendService { get; set; }
        [Inject]
        public IImageService ImageService { get; set; }

        public ICollection<TrendGetAllDto> Trends { get; set; }
        public ICollection<TrendGetAllDto> PopularTrends { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ICollection<TrendGetAllDto> col = await TrendService.GetPopular();
                if (col == null)
                {
                    return;
                }
                PopularTrends = new List<TrendGetAllDto>(col);
                ICollection<TrendGetAllDto> secondCol = await TrendService.GetAll();
                if (secondCol == null)
                {
                    return;
                }
                Trends = new List<TrendGetAllDto>(secondCol);

                foreach (TrendGetAllDto popularTrends in PopularTrends)
                {
                    TrendGetAllDto removedTrend = null;
                    foreach (TrendGetAllDto trend in Trends)
                    {
                        if (trend.Id == popularTrends.Id)
                        {
                            removedTrend = trend;
                            break;
                        }
                    }
                    if (removedTrend != null)
                    {
                        Trends.Remove(removedTrend);
                    }
                }

                StateHasChanged();
            }
        }

        protected void NavigateTrend(Guid trendId)
        {
            NavigationManager.NavigateTo($"/trends/{trendId}", forceLoad: true);
        }

        protected void NavigateNewTrend()
        {
            NavigationManager.NavigateTo($"/trends/create", forceLoad: true);
        }
    }
}
