using AutoMapper;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Models;
using TrendsViewer.Services;

namespace TrendsViewer.Pages
{
    public class TrendEditBase : ComponentBase
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
            Guid.TryParse(Id, out Guid trendId);

            if (trendId != Guid.Empty)
            {
                PageHeaderText = "Edit Trend";
                Trend = await TrendService.GetTrend(Guid.Parse(Id));
            } 
            else
            {
                PageHeaderText = "Create trend";
                Trend = new TrendDto
                {
                    Id = Guid.Empty,
                    Name = "NumeFrumos"
                };
            }

            Mapper.Map(Trend, EditTrendModel);
        }

        protected async void HandleValidSubmit()
        {
            Mapper.Map(EditTrendModel, Trend);

            TrendDto result = null;
            
            if (Trend.Id != Guid.Empty)
            {
                result = await TrendService.UpdateTrend((Guid) Trend.Id, Trend);
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
    }
}
