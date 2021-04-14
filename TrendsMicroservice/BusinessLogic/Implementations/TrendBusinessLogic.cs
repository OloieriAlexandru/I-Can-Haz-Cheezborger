using AutoMapper;
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

        private readonly IMapper mapper;

        public TrendBusinessLogic(IRepository<Trend> _trendRepository, IMapper _mapper)
        {
            trendRepository = _trendRepository;
            mapper = _mapper;
        }

        ICollection<TrendGetAllDto> ITrendBusinessLogic.GetAll()
        {
            ICollection<Trend> trends = trendRepository.GetAll();
            return mapper.Map<ICollection<TrendGetAllDto>>(trends);
        }

        TrendGetByIdDto ITrendBusinessLogic.GetById(Guid id)
        {
            Trend trend = trendRepository.GetById(id);
            if (trend == null)
            {
                return null;
            }
            return mapper.Map<TrendGetByIdDto>(trend);
        }

        TrendGetAllDto ITrendBusinessLogic.Create(TrendCreateDto trend)
        {
            Trend createdTrend = mapper.Map<Trend>(trend);

            trendRepository.Insert(createdTrend);
            trendRepository.SaveChanges();

            return mapper.Map<TrendGetAllDto>(createdTrend);
        }

        void ITrendBusinessLogic.Update(TrendUpdateDto trend)
        {
            Trend updatedTrend = trendRepository.GetById(trend.Id);
            if (updatedTrend == null)
            {
                return;
            }
            mapper.Map<TrendUpdateDto, Trend>(trend, updatedTrend);

            trendRepository.Update(updatedTrend);
            trendRepository.SaveChanges();
        }
    }
}
