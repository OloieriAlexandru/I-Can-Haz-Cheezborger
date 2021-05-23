using AutoMapper;
using Models.Auth;
using Models.Users;
using TrendsViewer.FormModels;

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

            CreateMap<LoginUserModel, AuthenticationRequest>();
        }
    }
}
