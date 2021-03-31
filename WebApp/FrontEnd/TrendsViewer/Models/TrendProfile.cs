using Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
