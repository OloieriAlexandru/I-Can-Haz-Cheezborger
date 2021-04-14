using AutoMapper;
using Models;

namespace TrendsViewer.Models
{
    public class TrendProfile : Profile
    {
        public TrendProfile()
        {
            CreateMap<TrendGetAllDto, EditTrendModel>()
                .ForMember(dest => dest.ConfirmName,
                           opt => opt.MapFrom(src => src.Name));
            CreateMap<EditTrendModel, TrendGetAllDto>();

            CreateMap<TrendUpdateDto, EditTrendModel>()
                .ForMember(dest => dest.ConfirmName,
                            opt => opt.MapFrom(src => src.Name));

            CreateMap<TrendGetByIdDto, TrendUpdateDto>();
            CreateMap<EditTrendModel, TrendUpdateDto>();
        }
    }
}
