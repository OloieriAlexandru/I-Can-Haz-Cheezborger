using AutoMapper;
using Entities;
using Models;

namespace BusinessLogic.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostGetByIdDto>();
            CreateMap<Post, PostGetAllDto>();
        }
    }
}
