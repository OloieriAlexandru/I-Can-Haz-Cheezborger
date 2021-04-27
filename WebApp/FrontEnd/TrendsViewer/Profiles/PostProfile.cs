using AutoMapper;
using Models.Posts;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateDto, CreatePostModel>();
            CreateMap<CreatePostModel, PostCreateDto>();

            CreateMap<PostGetByIdDto, UpdatePostModel>();
            CreateMap<EditPostModel, PostUpdateDto>();

            CreateMap<PostUpdateDto, EditPostModel>();

            CreateMap<PostGetByIdDto, PostUpdateDto>();
            CreateMap<EditPostModel, PostUpdateDto>();
        }
    }
}
