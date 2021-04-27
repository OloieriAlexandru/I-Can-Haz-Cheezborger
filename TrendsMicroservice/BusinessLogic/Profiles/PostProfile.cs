using AutoMapper;
using Entities;
using Models.Posts;

namespace BusinessLogic.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostGetByIdDto>();
            CreateMap<Post, PostGetAllDto>();

            CreateMap<PostCreateDto, Post>();
            CreateMap<PostUpdateDto, Post>();
        }
    }
}
