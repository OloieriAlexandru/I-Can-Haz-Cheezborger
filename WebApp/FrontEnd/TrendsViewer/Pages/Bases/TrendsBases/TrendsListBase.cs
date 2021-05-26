using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<TrendGetAllDto> TrendsList { get; set; }

        public const int PAGESIZE = 10;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Trends = await TrendService.GetAll();
                TrendsList = Trends.Take(PAGESIZE).ToList();
                TotalPages = (int)Math.Ceiling(Trends.Count() / (decimal)PAGESIZE);

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

        public void UpdateList(int pageNumber)
        {
            CurrentPage = pageNumber;
            TrendsList = Trends.Skip(pageNumber * PAGESIZE).Take(PAGESIZE).ToList();
        }

        public void NavigateTo(string direction)
        {
            if (direction == "prev" && CurrentPage != 0)
                CurrentPage -= 1;
            if (direction == "next" && CurrentPage != TotalPages - 1)
                CurrentPage += 1;
            if (direction == "first")
                CurrentPage = 0;
            if (direction == "last")
                CurrentPage = TotalPages - 1;

            UpdateList(CurrentPage);
        }
    }
}
