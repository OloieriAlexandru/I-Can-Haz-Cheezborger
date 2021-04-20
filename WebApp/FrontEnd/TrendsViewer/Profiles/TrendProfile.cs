using AutoMapper;
using Models;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
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
        }
    }
}
