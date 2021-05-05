using AutoMapper;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using DataAccess.Seed;
using Entities;
using Models.Trends;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class TrendBusinessLogic : ITrendBusinessLogic
    {
        private readonly IRepository<Trend> trendRepository;

        private readonly IRepository<TrendFollow> trendFollowRepository;

        private readonly IMapper mapper;

        public TrendBusinessLogic(IRepository<Trend> trendRepository, IRepository<TrendFollow> trendFollowRepository,
            IMapper mapper)
        {
            this.trendRepository = trendRepository;
            this.trendFollowRepository = trendFollowRepository;
            this.mapper = mapper;
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

        void ITrendBusinessLogic.Delete(Guid id)
        {
            trendRepository.Delete(id);
            trendRepository.SaveChanges();
        }

        void ITrendBusinessLogic.PatchFollow(TrendPatchFollowDto trendPatchFollowDto)
        {
            TrendFollow trendFollow = trendFollowRepository.GetByFilter(
                tf => tf.UserId == trendPatchFollowDto.CreatorId && tf.TrendId == trendPatchFollowDto.Id);
            bool doFollow = trendPatchFollowDto.Type == "Follow" ? true : false;

            if (trendFollow == null && doFollow)
            {
                trendFollow = new TrendFollow
                {
                    TrendId = trendPatchFollowDto.Id,
                    UserId = trendPatchFollowDto.CreatorId
                };
                trendFollowRepository.Insert(trendFollow);
                trendFollowRepository.SaveChanges();
            }
            else if (trendFollow != null && !doFollow)
            {
                trendFollowRepository.Delete(trendFollow.Id);
                trendFollowRepository.SaveChanges();
            }
        }

        ICollection<TrendGetAllDto> ITrendBusinessLogic.GetPopular()
        {
            return mapper.Map<ICollection<TrendGetAllDto>>(TrendsSeed.Seed());
        }
    }
}
