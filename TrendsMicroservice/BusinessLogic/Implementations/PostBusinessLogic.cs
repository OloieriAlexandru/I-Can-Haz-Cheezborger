using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Utils;
using DataAccess.Abstractions;
using Entities;
using Models;
using Models.Common;
using Models.Images;
using Models.Models;
using Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Implementations
{
    public class PostBusinessLogic : IPostBusinessLogic
    {
        private readonly IRepository<Post> postRepository;

        private readonly IRepository<PostReact> postReactRepository;

        private readonly IMapper mapper;
        
        private readonly IContentScanTaskService contentScanTaskService;

        private readonly IImageService imageService;

        public PostBusinessLogic(IRepository<Post> postRepository, IRepository<PostReact> postReactRepository,
            IMapper mapper, IContentScanTaskService contentScanTaskService, IImageService imageService)
        {
            this.postRepository = postRepository;
            this.postReactRepository = postReactRepository;
            this.mapper = mapper;
            this.contentScanTaskService = contentScanTaskService;
            this.imageService = imageService;
        }

        ICollection<PostGetAllDto> IPostBusinessLogic.GetAll(Guid trendId, UserInfoModel userInfo)
        {
            ICollection<Post> posts = postRepository.GetAllByFilter(p => p.TrendId == trendId, "Reacts");
            ICollection<PostGetAllDto> returnedPosts = new List<PostGetAllDto>();

            foreach (Post post in posts)
            {
                PostGetAllDto postGetAllDto = mapper.Map<PostGetAllDto>(post);
                if (userInfo != null && post.Reacts != null)
                {
                    PostReact react = post.Reacts.FirstOrDefault(pr => pr.UserId == userInfo.CreatorId);
                    if (react != null)
                    {
                        if (react.Type == ReactType.Like)
                        {
                            postGetAllDto.Liked = true;
                        }
                        else if (react.Type == ReactType.Dislike)
                        {
                            postGetAllDto.Disliked = true;
                        }
                    }
                }
                returnedPosts.Add(postGetAllDto);
            }

            return returnedPosts;
        }

        PostGetByIdDto IPostBusinessLogic.GetById(Guid id, UserInfoModel userInfo)
        {
            Post post = postRepository.GetById(id, "Reacts");
            if (post == null)
            {
                return null;
            }
            PostGetByIdDto returnedPost = mapper.Map<PostGetByIdDto>(post);
            if (userInfo != null && post.Reacts != null)
            {
                PostReact react = post.Reacts.FirstOrDefault(pr => pr.UserId == userInfo.CreatorId);
                if (react != null)
                {
                    if (react.Type == ReactType.Like)
                    {
                        returnedPost.Liked = true;
                    }
                    else if (react.Type == ReactType.Dislike)
                    {
                        returnedPost.Disliked = true;
                    }
                }
            }
            return returnedPost;
        }

        PostGetAllDto IPostBusinessLogic.Create(PostCreateDto post)
        {
            Post createdPost = mapper.Map<Post>(post);

            ImageGetDto image = imageService.Create(new ImageCreateDto()
            {
                Image = post.MediaPath,
                Prefix = "posts"
            });

            CreateContentScanTaskDto createContentScanTaskDto = new CreateContentScanTaskDto()
            {
                ImageUrl = image != null ? imageService.GetFullImageUrl(image.Url) : null,
                Text = post.Description,
                CallbackUrl = $"/api/v1/trends/{post.TrendId}/posts/{createdPost.Id}/content-scan-result"
            };
            contentScanTaskService.CreateTask(createContentScanTaskDto);

            createdPost.MediaPath = image != null ? image.Url : imageService.GetDefaultImageUrl();
            postRepository.Insert(createdPost);
            postRepository.SaveChanges();

            return mapper.Map<PostGetAllDto>(createdPost);
        }

        void IPostBusinessLogic.Patch(PostPatchDto post)
        {
            Post updatedPost = postRepository.GetById(post.Id);
            if (updatedPost == null)
            {
                return;
            }
            mapper.Map<PostPatchDto, Post>(post, updatedPost);

            postRepository.Update(updatedPost);
            postRepository.SaveChanges();
        }

        void IPostBusinessLogic.PatchReact(PostPatchReactDto postPatchReact)
        {
            PostReact postReact = postReactRepository.GetByFilter(
                pr => pr.UserId == postPatchReact.CreatorId && pr.PostId == postPatchReact.Id);
            ReactType type = (ReactType)System.Enum.Parse(typeof(ReactType), postPatchReact.Type);

            int deltaUpvotes = 0, deltaDownvotes = 0;
            if (postReact == null)
            {
                postReact = new PostReact
                {
                    PostId = postPatchReact.Id,
                    UserId = postPatchReact.CreatorId,
                    Type = type
                };
                postReactRepository.Insert(postReact);
                postReactRepository.SaveChanges();
                ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, ReactType.None);
            }
            else
            {
                if (type == ReactType.None)
                {
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, postReact.Type);
                    postReactRepository.Delete(postReact.Id);
                    postReactRepository.SaveChanges();
                }
                else if (type != postReact.Type)
                {
                    ReactsUtils.UpdateDeltas(ref deltaUpvotes, ref deltaDownvotes, type, postReact.Type);
                    postReact.Type = type;
                    postReactRepository.Update(postReact);
                    postReactRepository.SaveChanges();
                }
            }
            if (deltaUpvotes != 0 || deltaDownvotes != 0)
            {
                Post post = postRepository.GetById(postPatchReact.Id);
                post.Upvotes += deltaUpvotes;
                post.Downvotes += deltaDownvotes;
                postRepository.Update(post);
                postRepository.SaveChanges();
            }
        }

        void IPostBusinessLogic.PatchContentScanTaskApprovals(Guid id, PatchContentScanTaskApprovalsDto taskApprovalsDto)
        {
            Post post = postRepository.GetById(id);
            post.ApprovedText = taskApprovalsDto.ApprovedText;
            post.ApprovedImage = taskApprovalsDto.ApprovedImage;
            postRepository.Update(post);
            postRepository.SaveChanges();
        }

        void IPostBusinessLogic.Delete(Guid id)
        {
            postRepository.Delete(id);
            postRepository.SaveChanges();
        }

        ICollection<PostGetAllDto> IPostBusinessLogic.GetRecommended(UserInfoModel userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
