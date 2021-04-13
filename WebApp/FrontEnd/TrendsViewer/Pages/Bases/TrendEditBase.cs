﻿using AutoMapper;
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
            if (!Guid.TryParse(Id, out Guid trendId))
            {
                throw new InvalidProgramException("Invalid Id!");
            }
            PageHeaderText = "Edit Trend";
            Trend = await TrendService.GetTrend(Guid.Parse(Id));


            Mapper.Map(Trend, EditTrendModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditTrendModel, Trend);
            TrendDto result = null;
            result = await TrendService.UpdateTrend((Guid) Trend.Id, Trend);
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
