using Models;
using AutoMapper;

namespace TrendsViewer.Models
{
    public class TrendProfile : Profile
    {
        public TrendProfile()
        {
            CreateMap<TrendDto, EditTrendModel>()
                .ForMember(dest => dest.ConfirmName,
                           opt => opt.MapFrom(src => src.Name));
            CreateMap<EditTrendModel, TrendDto>();
        }
    }
}
