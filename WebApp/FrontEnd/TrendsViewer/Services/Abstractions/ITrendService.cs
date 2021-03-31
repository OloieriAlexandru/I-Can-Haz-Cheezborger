using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services
{
    public interface ITrendService
    {
        Task<IEnumerable<TrendDto>> GetTrends();
        Task<TrendDto> GetTrend(Guid id);
        Task<TrendDto> UpdateTrend(Guid id, TrendDto updatedTrend);
        Task<TrendDto> CreateTrend(TrendDto newTrend);
        Task DeleteTrend(Guid id);
    }
}
