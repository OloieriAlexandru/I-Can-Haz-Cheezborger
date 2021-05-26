using Models.Posts;
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

        private readonly IAuthService authService;

        public TrendService(HttpServiceResolver httpServiceResolver, IAuthService authService)
        {
            httpService = httpServiceResolver("trends");

            this.authService = authService;
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

        async Task<ICollection<TrendGetAllDto>> ITrendService.GetAll()
        {
            string url = "api/v1/trends";
            await authService.Initialize();
            if (authService.IsLoggedIn())
            {
                url += "/auth";
            }
            return await httpService.Get<TrendGetAllDto[]>(url);
        }

        async Task ITrendService.UpdateTrend(Guid id, TrendUpdateDto updatedTrend)
        {
            await httpService.Put<ValueTask>($"api/v1/trends/{id}", updatedTrend);
        }

        async Task<ICollection<TrendGetAllDto>> ITrendService.GetPopular()
        {
            return await httpService.Get<TrendGetAllDto[]>("/api/v1/trends/popular");
        }

        async Task<ICollection<PostGetAllDto>> ITrendService.GetRecomended(string UserId)
        {
            return await httpService.Get<PostGetAllDto[]>("/api/v1/trends/" + UserId + "/recomended");
        }

        async Task ITrendService.PatchTrendFollow(Guid id, TrendPatchFollowDto trendPatchFollowDto)
        {
            await httpService.Patch<ValueTask>($"/api/v1/trends/{id}/follow", trendPatchFollowDto);
        }
    }
}
