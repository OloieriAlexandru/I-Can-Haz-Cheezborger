using Microsoft.AspNetCore.Components;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendsListBase : ComponentBase
    {
        [Inject]
        public ITrendService TrendService { get; set; }
        public IEnumerable<TrendDto> Trends { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Trends = (await TrendService.GetTrends()).ToList();
        }
    }
}
