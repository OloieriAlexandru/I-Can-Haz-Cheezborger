using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrendsViewer.Services
{
    public class TrendService : ITrendService
    {
        private readonly HttpClient httpClient;
        public TrendService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<TrendDto>> GetTrends()
        {
            return await httpClient.GetJsonAsync<TrendDto[]>("api/v1/trends");
        }

        public async Task<TrendDto> GetTrend(Guid id)
        {
            return await httpClient.GetJsonAsync<TrendDto>($"api/v1/trends/{id}");
        }

        public Task<TrendDto> UpdateTrend(Guid id, TrendDto updatedTrend)
        {
            return (Task<TrendDto>)httpClient.PutJsonAsync($"api/v1/trends/{id}", updatedTrend);
        }


        public async Task<TrendDto> CreateTrend(TrendDto newTrend)
        {
            return await httpClient.PostJsonAsync<TrendDto>("api/v1/trends", newTrend);
        }

        public async Task DeleteTrend(Guid id)
        {
            await httpClient.DeleteAsync($"api/v1/trends/{id}");
        }
    }
}
