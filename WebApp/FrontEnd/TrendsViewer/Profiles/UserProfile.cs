using AutoMapper;
using Models.Users;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, RegisterUserModel>()
                .ForMember(dest => dest.PasswordCheck,
                           opt => opt.MapFrom(src => src.Password));
            CreateMap<RegisterUserModel, UserCreateDto>();
        }
    }
}
