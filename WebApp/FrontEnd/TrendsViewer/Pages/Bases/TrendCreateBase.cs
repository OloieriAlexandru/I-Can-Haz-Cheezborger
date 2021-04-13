using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendCreateBase : ComponentBase
    {
        private TrendDto Trend { get; set; } = new TrendDto();
        public EditTrendModel EditTrendModel { get; set; } = new EditTrendModel();
        [Inject]
        public ITrendService TrendService { get; set; }

        public string PageHeaderText { get; set; }

        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.PageHeaderText = "Create a new trend";   
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditTrendModel, Trend);

            TrendDto result = null;

            if (Trend.Id != Guid.Empty)
            {
                result = await TrendService.UpdateTrend((Guid)Trend.Id, Trend);
            }
            else
            {
                result = await TrendService.CreateTrend(Trend);
            }

            if (result != null)
            {
                NavigationManager.NavigateTo("/trends");
            }
        }

        protected async Task DeleteClick()
        {
            await TrendService.DeleteTrend((Guid)Trend.Id);
            NavigationManager.NavigateTo("/trends");
        }

        protected async Task CreateTrend()
        {
            this.Trend.Name = EditTrendModel.Name;
            await TrendService.CreateTrend(Trend);
            NavigationManager.NavigateTo("/trends");
        }
    }
}
