using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Services.Abstractions;

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (!Guid.TryParse(Id, out Guid trendId))
                {
                    throw new InvalidProgramException("Invalid Id!");
                }
                TrendGetByIdDto updatedTrend = await TrendService.GetById(Guid.Parse(Id));
                Trend = Mapper.Map<TrendUpdateDto>(updatedTrend);
                Mapper.Map(Trend, EditTrendModel);
            }
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditTrendModel, Trend);
            String date = DateTime.Now.ToString("dd-M-yyyy HH:mm");
            Trend.DateTime = date;

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
