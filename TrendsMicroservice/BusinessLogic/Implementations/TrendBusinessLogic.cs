using AutoMapper;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using DataAccess.Seed;
using Entities;
using Models;
using Models.Common;
using Models.Images;
using Models.Models;
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

        private readonly IContentScanTaskService contentScanTaskService;

        private readonly IImageService imageService;

        public TrendBusinessLogic(IRepository<Trend> trendRepository, IRepository<TrendFollow> trendFollowRepository,
            IMapper mapper, IContentScanTaskService contentScanTaskService, IImageService imageService)
        {
            this.trendRepository = trendRepository;
            this.trendFollowRepository = trendFollowRepository;
            this.mapper = mapper;
            this.contentScanTaskService = contentScanTaskService;
            this.imageService = imageService;
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

            ImageGetDto image = imageService.Create(new ImageCreateDto()
            {
                Image = trend.Image,
                Prefix = "trends"
            });

            CreateContentScanTaskDto createContentScanTaskDto = new CreateContentScanTaskDto()
            {
                ImageUrl = image != null ? imageService.GetFullImageUrl(image.Url) : null,
                Text = trend.Description,
                CallbackUrl = $"/api/v1/trends/{createdTrend.Id}/content-scan-result"
            };
            contentScanTaskService.CreateTask(createContentScanTaskDto);

            createdTrend.ImageUrl = image != null ? image.Url : imageService.GetDefaultImageUrl();
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

        void ITrendBusinessLogic.PatchContentScanTaskApprovals(Guid id, PatchContentScanTaskApprovalsDto taskApprovalsDto)
        {
            Trend trend = trendRepository.GetById(id);
            trend.ApprovedImage = taskApprovalsDto.ApprovedImage;
            trend.ApprovedText = taskApprovalsDto.ApprovedText;
            trendRepository.Update(trend);
            trendRepository.SaveChanges();
        }
    }
}
