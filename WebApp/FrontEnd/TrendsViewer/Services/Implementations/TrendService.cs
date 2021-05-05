using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Resolvers;

namespace TrendsViewer.Services.Implementations
{
    public class TrendService : ITrendService
    {
        private readonly IHttpService httpService;

        public TrendService(HttpServiceResolver httpServiceResolver)
        {
            httpService = httpServiceResolver("trends");
        }

        async Task<TrendGetAllDto> ITrendService.CreateTrend(TrendCreateDto newTrend)
        {
            return await httpService.Post<TrendGetAllDto>("api/v1/trends", newTrend);
        }

        async Task ITrendService.DeleteTrend(Guid id)
        {
            await httpService.Delete<ValueTask>($"api/v1/trends/{id}");
        }

        async Task<TrendGetByIdDto> ITrendService.GetById(Guid id)
        {
            return await httpService.Get<TrendGetByIdDto>($"api/v1/trends/{id}");
        }

        async Task<IEnumerable<TrendGetAllDto>> ITrendService.GetAll()
        {
            return await httpService.Get<TrendGetAllDto[]>("api/v1/trends");
        }

        async Task ITrendService.UpdateTrend(Guid id, TrendUpdateDto updatedTrend)
        {
            await httpService.Put<ValueTask>($"api/v1/trends/{id}", updatedTrend);
        }

        async Task<IEnumerable<TrendGetAllDto>> ITrendService.GetPopular()
        {
            return await httpService.Get<TrendGetAllDto[]>("/api/v1/trends/popular");
        }

        async Task ITrendService.UpdateTrendReact(Guid id, TrendPatchFollowDto trendPatchFollowDto)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }
    }
}
