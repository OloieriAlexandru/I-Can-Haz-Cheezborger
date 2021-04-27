using AutoMapper;
using Entities;
using Models.Trends;

namespace BusinessLogic.Profiles
{
    public class TrendProfile : Profile
    {
        public TrendProfile()
        {
            CreateMap<Trend, TrendGetAllDto>();
            CreateMap<Trend, TrendGetByIdDto>();

            CreateMap<TrendCreateDto, Trend>();
            CreateMap<TrendUpdateDto, Trend>();
        }
    }
}
