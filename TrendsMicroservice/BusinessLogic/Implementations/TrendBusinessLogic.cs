using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Entities;
using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class TrendBusinessLogic : ITrendBusinessLogic
    {
        private readonly IRepository<Trend> trendRepository;

        public TrendBusinessLogic(IRepository<Trend> _trendRepository)
        {
            trendRepository = _trendRepository;
        }

        ICollection<TrendDto> ITrendBusinessLogic.GetAll()
        {
            ICollection<Trend> trends = trendRepository.GetAll();
            ICollection<TrendDto> trendDtos = new List<TrendDto>();

            foreach (Trend t in trends)
            {
                trendDtos.Add(new TrendDto()
                {
                    Id = t.Id,
                    Name = t.Name
                });
            }
            return trendDtos;
        }

        TrendDto ITrendBusinessLogic.GetById(Guid id)
        {
            Trend trend = trendRepository.GetById(id);
            TrendDto trendDto = null;

            if (trend != null)
            {
                trendDto = new TrendDto() {
                    Id = trend.Id,
                    Name = trend.Name
                };
            }
            return trendDto;
        }

        void ITrendBusinessLogic.Create(TrendDto trend)
        {
            Trend newTrend = new Trend()
            {
                Name = trend.Name
            };
            trendRepository.Insert(newTrend);
            trendRepository.SaveChanges();
            trend.Id = newTrend.Id;
        }

        void ITrendBusinessLogic.Update(TrendDto trend)
        {
            Trend updatedTrend = new Trend()
            {
                Id = trend.Id.Value,
                Name = trend.Name
            };
            trendRepository.Update(updatedTrend);
            trendRepository.SaveChanges();
        }
    }
}
