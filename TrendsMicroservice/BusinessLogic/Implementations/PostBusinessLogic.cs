using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Entities;
using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Implementations
{
    public class PostBusinessLogic : IPostBusinessLogic
    {
        private readonly IRepository<Post> postRepository;

        public PostBusinessLogic(IRepository<Post> _postRepository)
        {
            postRepository = _postRepository;
        }

        ICollection<PostDto> IPostBusinessLogic.GetAll()
        {
            ICollection<Post> posts = postRepository.GetAll();
            ICollection<PostDto> postDtos = new List<PostDto>();

            foreach (Post p in posts)
            {
                TrendDto _trendDto = new TrendDto()
                {
                    Id = p.Trend.Id,
                    Name = p.Trend.Name
                };
                postDtos.Add(new PostDto()
                {
                    Id = p.Id,
                    Title = p.Title,
                    MediaPath = p.MediaPath,
                    Upvotes = p.Upvotes,
                    Downvotes = p.Downvotes,
                    TrendId = p.TrendId,
                    TrendDto = _trendDto
                });
            }
            return postDtos;
        }

        PostDto IPostBusinessLogic.GetById(Guid id)
        {
            Post post = postRepository.GetById(id);
            PostDto postDto = null;

            if (post != null)
            {
                TrendDto _trendDto = new TrendDto()
                {
                    Id = post.Trend.Id,
                    Name = post.Trend.Name
                };
                postDto = new PostDto()
                {
                    Id = post.Id,
                    Title = post.Title,
                    MediaPath = post.MediaPath,
                    Upvotes = post.Upvotes,
                    Downvotes = post.Downvotes,
                    TrendId = post.TrendId,
                    TrendDto = _trendDto
                };
            }
            return postDto;
        }

        void IPostBusinessLogic.Create(PostDto post)
        {
            Trend _trend = new Trend()
            {
                Name = post.TrendDto.Name
            };
            Post newPost = new Post()
            {
                Title = post.Title,
                MediaPath = post.MediaPath,
                Upvotes = post.Upvotes,
                Downvotes = post.Downvotes,
                TrendId = post.TrendId,
                Trend = _trend
            };
            postRepository.Insert(newPost);
            postRepository.SaveChanges();
            post.Id = newPost.Id;
        }

        void IPostBusinessLogic.Update(PostDto post)
        {
            Trend _trend = new Trend()
            {
                Name = post.TrendDto.Name
            };
            Post updatedPost = new Post()
            {
                Id = post.Id.Value,
                Title = post.Title,
                MediaPath = post.MediaPath,
                Upvotes = post.Upvotes,
                Downvotes = post.Downvotes,
                TrendId = post.TrendId,
                Trend = _trend
            };
            postRepository.Update(updatedPost);
            postRepository.SaveChanges();
        }
    }
}
