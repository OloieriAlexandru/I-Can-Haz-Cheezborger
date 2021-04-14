using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public TrendGetByIdDto Trend { get; set; }
        public IEnumerable<PostGetAllDto> Posts { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Trend = await TrendService.GetTrend(Guid.Parse(Id));
            Posts = Trend.Posts;
        }
    }
}