using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendEditBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public ITrendService TrendService { get; set; }

        [Parameter]
        public string Id { get; set; }

        public EditTrendModel EditTrendModel { get; set; }

        private TrendUpdateDto Trend { get; set; }

        public TrendEditBase()
        {
            EditTrendModel = new EditTrendModel();
            Trend = new TrendUpdateDto();
        }

        protected async override Task OnInitializedAsync()
        {
            if (!Guid.TryParse(Id, out Guid trendId))
            {
                throw new InvalidProgramException("Invalid Id!");
            }
            TrendGetByIdDto updatedTrend = await TrendService.GetTrend(Guid.Parse(Id));
            Trend = Mapper.Map<TrendUpdateDto>(updatedTrend);
            Mapper.Map(Trend, EditTrendModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditTrendModel, Trend);
            await TrendService.UpdateTrend(Trend.Id, Trend);
            NavigationManager.NavigateTo("/trends");
        }

        protected async Task DeleteClick()
        {
            await TrendService.DeleteTrend(Trend.Id);
            NavigationManager.NavigateTo("/trends");
        }
    }
}
