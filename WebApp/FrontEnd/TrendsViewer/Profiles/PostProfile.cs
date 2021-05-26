using AutoMapper;
using Models.Posts;
using TrendsViewer.FormModels;

namespace TrendsViewer.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreateDto, CreatePostModel>();
            CreateMap<CreatePostModel, PostCreateDto>();

            CreateMap<PostGetByIdDto, UpdatePostModel>();
            CreateMap<EditPostModel, PostPatchDto>();

            CreateMap<PostPatchDto, EditPostModel>();

            CreateMap<PostGetByIdDto, PostPatchDto>();
            CreateMap<EditPostModel, PostPatchDto>();

            CreateMap<PostGetByIdDto, PostGetAllDto>();
        }
    }
}
