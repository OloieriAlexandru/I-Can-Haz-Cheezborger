using Microsoft.AspNetCore.Components;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendsListBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }

        public IEnumerable<TrendGetAllDto> Trends { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Trends = await TrendService.GetTrends();
        }
    }
}
