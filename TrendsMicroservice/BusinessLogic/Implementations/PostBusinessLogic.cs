using AutoMapper;
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

        private readonly IMapper mapper;

        public PostBusinessLogic(IRepository<Post> _postRepository, IMapper _mapper)
        {
            postRepository = _postRepository;
            mapper = _mapper;
        }

        ICollection<PostGetAllDto> IPostBusinessLogic.GetAll(Guid trendId)
        {
            ICollection<Post> posts = postRepository.GetAllByFilter(p => p.TrendId == trendId);
            return mapper.Map<ICollection<PostGetAllDto>>(posts);
        }

        PostGetByIdDto IPostBusinessLogic.GetById(Guid id)
        {
            Post post = postRepository.GetById(id);
            if (post == null)
            {
                return null;
            }
            return mapper.Map<PostGetByIdDto>(post);
        }

        PostGetAllDto IPostBusinessLogic.Create(PostCreateDto post)
        {
            Post createdPost = mapper.Map<Post>(post);
            
            postRepository.Insert(createdPost);
            postRepository.SaveChanges();

            return mapper.Map<PostGetAllDto>(createdPost);
        }

        void IPostBusinessLogic.Update(PostUpdateDto post)
        {
            Post updatedPost = postRepository.GetById(post.Id);
            if (updatedPost == null)
            {
                return;
            }
            mapper.Map<PostUpdateDto, Post>(post, updatedPost);

            postRepository.Update(updatedPost);
            postRepository.SaveChanges();
        }

        public void Delete(Guid id)
        {
            postRepository.Delete(id);
            postRepository.SaveChanges();
        }
    }
}
