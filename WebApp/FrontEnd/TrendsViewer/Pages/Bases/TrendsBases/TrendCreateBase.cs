using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models.Trends;
using System.Threading.Tasks;
using TrendsViewer.FormModels;
using TrendsViewer.Services.Abstractions;

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

        public string Image { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Question_mark_%28black%29.svg/1200px-Question_mark_%28black%29.svg.png";

        public TrendGetAllDto Trend { get; set; }

        public TrendCreateBase()
        {
            CreateTrendModel = new CreateTrendModel();
            Trend = new TrendGetAllDto();
        }

        protected void ImageSelected(string image)
        {
            Image = image;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            TrendCreateDto trend = new TrendCreateDto();
            Mapper.Map(CreateTrendModel, trend);
            trend.Image = Image;

            await TrendService.CreateTrend(trend);
            NavigationManager.NavigateTo("/trends", forceLoad: false);
        }
    }
}
