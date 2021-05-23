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
        public IImageService ImageService { get; set; }

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

        protected void NavigateToTrendPage(TrendGetAllDto trend)
        {
            NavigationManager.NavigateTo($"/trends/{trend.Id}", forceLoad: true);
        }

        protected async Task FollowTrend(TrendGetAllDto trend)
        {
            TrendPatchFollowDto trendPatchFollowDto = new TrendPatchFollowDto()
            {
                Id = trend.Id
            };
            if (trend.Followed)
            {
                --trend.FollowersCount;
                trendPatchFollowDto.Type = "Unfollow";
            }
            else
            {
                ++trend.FollowersCount;
                trendPatchFollowDto.Type = "Follow";
            }
            trend.Followed = !trend.Followed;
            StateHasChanged();

            await TrendService.PatchTrendFollow(trend.Id, trendPatchFollowDto);
        }
    }
}
