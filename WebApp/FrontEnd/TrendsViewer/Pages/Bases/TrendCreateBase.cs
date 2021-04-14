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
        [Inject]
        public ITrendService TrendService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }

        public CreateTrendModel CreateTrendModel { get; set; }

        private TrendCreateDto Trend { get; set; }

        public TrendCreateBase()
        {
            CreateTrendModel = new CreateTrendModel();
            Trend = new TrendCreateDto();
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(CreateTrendModel, Trend);

            await TrendService.CreateTrend(Trend);
            NavigationManager.NavigateTo("/trends");
        }
    }
}
