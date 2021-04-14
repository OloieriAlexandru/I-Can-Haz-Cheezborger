using AutoMapper;
using Models;

namespace TrendsViewer.Models
{
    public class TrendProfile : Profile
    {
        public TrendProfile()
        {
            CreateMap<TrendGetAllDto, EditTrendModel>();
            
            CreateMap<EditTrendModel, TrendGetAllDto>();
            CreateMap<TrendUpdateDto, EditTrendModel>();

            CreateMap<TrendGetByIdDto, TrendUpdateDto>();
            CreateMap<EditTrendModel, TrendUpdateDto>();

            CreateMap<TrendCreateDto, CreateTrendModel>();
            CreateMap<CreateTrendModel, TrendCreateDto>();

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
