using Models.Trends;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface ITrendService
    {
        Task<IEnumerable<TrendGetAllDto>> GetTrends();

        Task<TrendGetByIdDto> GetTrend(Guid id);
        
        Task UpdateTrend(Guid id, TrendUpdateDto updatedTrend);
        
        Task<TrendGetAllDto> CreateTrend(TrendCreateDto newTrend);
        
        Task DeleteTrend(Guid id);
    }
}
