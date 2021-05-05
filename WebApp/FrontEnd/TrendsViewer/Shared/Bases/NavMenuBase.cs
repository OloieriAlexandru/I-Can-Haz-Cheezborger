using Microsoft.AspNetCore.Components;
using Models.Trends;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Shared.Bases
{
    public class NavMenuBase : ComponentBase
    {
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
    }
}
