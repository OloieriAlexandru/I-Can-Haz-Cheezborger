using Microsoft.AspNetCore.Components;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class TrendService : ITrendService
    {
        private readonly HttpClient httpClient;

        public TrendService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        async Task<TrendGetAllDto> ITrendService.CreateTrend(TrendCreateDto newTrend)
        {
            return await httpClient.PostJsonAsync<TrendGetAllDto>("api/v1/trends", newTrend);
        }

        async Task ITrendService.DeleteTrend(Guid id)
        {
            await httpClient.DeleteAsync($"api/v1/trends/{id}");
        }

        async Task<TrendGetByIdDto> ITrendService.GetTrend(Guid id)
        {
            return await httpClient.GetJsonAsync<TrendGetByIdDto>($"api/v1/trends/{id}");
        }

        async Task<IEnumerable<TrendGetAllDto>> ITrendService.GetTrends()
        {
            return await httpClient.GetJsonAsync<TrendGetAllDto[]>("api/v1/trends");
        }

        async Task ITrendService.UpdateTrend(Guid id, TrendUpdateDto updatedTrend)
        {
            await httpClient.PutJsonAsync($"api/v1/trends/{id}", updatedTrend);
        }
    }
}
