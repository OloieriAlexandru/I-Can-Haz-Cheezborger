using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Threading.Tasks;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendDetailsBase : ComponentBase
    {

        public TrendDto Trend { get; set; } = new TrendDto();
        [Inject]
        public ITrendService TrendService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Trend = await TrendService.GetTrend(Guid.Parse(Id));
        }
    }
}
