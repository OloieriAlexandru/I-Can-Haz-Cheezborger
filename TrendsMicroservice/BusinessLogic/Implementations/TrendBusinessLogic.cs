using AutoMapper;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using DataAccess.Seed;
using Entities;
using Models;
using Models.Trends;
using System;
using System.Collections.Generic;
using System.Linq;

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

        ICollection<TrendGetAllDto> ITrendBusinessLogic.GetAll(UserInfoModel userInfo)
        {
            ICollection<Trend> trends = trendRepository.GetAll("Follows");
            ICollection<TrendGetAllDto> returnedTrends = new List<TrendGetAllDto>();

            foreach (Trend trend in trends)
            {
                TrendGetAllDto trendGetAllDto = mapper.Map<TrendGetAllDto>(trend);
                if (userInfo != null && trend.Follows != null)
                {
                    TrendFollow follow = trend.Follows.FirstOrDefault(f => f.UserId == userInfo.CreatorId);
                    if (follow != null)
                    {
                        trendGetAllDto.Followed = true;
                    }
                }
                returnedTrends.Add(trendGetAllDto);
            }

            return returnedTrends;
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
            bool doFollow = trendPatchFollowDto.Type == "Follow";

            int followsDelta = 0;
            if (trendFollow == null && doFollow)
            {
                trendFollow = new TrendFollow
                {
                    TrendId = trendPatchFollowDto.Id,
                    UserId = trendPatchFollowDto.CreatorId
                };
                trendFollowRepository.Insert(trendFollow);
                trendFollowRepository.SaveChanges();
                ++followsDelta;
            }
            else if (trendFollow != null && !doFollow)
            {
                trendFollowRepository.Delete(trendFollow.Id);
                trendFollowRepository.SaveChanges();
                --followsDelta;
            }
            if (followsDelta != 0)
            {
                Trend trend = trendRepository.GetById(trendPatchFollowDto.Id);
                trend.FollowersCount += followsDelta;
                trendRepository.Update(trend);
                trendRepository.SaveChanges();
            }
        }

        ICollection<TrendGetAllDto> ITrendBusinessLogic.GetPopular()
        {
            return mapper.Map<ICollection<TrendGetAllDto>>(TrendsSeed.Seed());
        }
    }
}
