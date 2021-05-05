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

        Task<TrendGetByIdDto> GetById(Guid id);
        
        Task UpdateTrend(Guid id, TrendUpdateDto updatedTrend);

        Task UpdateTrendReact(Guid id, TrendPatchFollowDto trendPatchFollowDto);

        Task<TrendGetAllDto> CreateTrend(TrendCreateDto newTrend);
        
        Task DeleteTrend(Guid id);
    }
}
