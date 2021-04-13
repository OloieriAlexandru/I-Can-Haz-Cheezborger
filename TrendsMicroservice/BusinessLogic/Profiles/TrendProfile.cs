using AutoMapper;
using Entities;
using Models;

namespace BusinessLogic.Profiles
{
    public class TrendProfile : Profile
    {
        public TrendProfile()
        {
            CreateMap<Trend, TrendGetAllDto>();
            CreateMap<Trend, TrendGetByIdDto>();
        }
    }
}
