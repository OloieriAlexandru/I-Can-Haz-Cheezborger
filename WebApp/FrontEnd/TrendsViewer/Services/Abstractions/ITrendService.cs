using Models.Posts;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface ITrendService
    {
        Task<IEnumerable<TrendGetAllDto>> GetAll();

        Task<IEnumerable<TrendGetAllDto>> GetPopular();

        Task<IEnumerable<PostGetAllDto>> GetRecomended(string UserId);

        Task<TrendGetByIdDto> GetById(Guid id);
        
        Task UpdateTrend(Guid id, TrendUpdateDto updatedTrend);

        Task PatchTrendFollow(Guid id, TrendPatchFollowDto trendPatchFollowDto);

        Task<TrendGetAllDto> CreateTrend(TrendCreateDto newTrend);
        
        Task DeleteTrend(Guid id);
    }
}
