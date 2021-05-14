using AutoMapper;
using Entities;
using Models.Auth;
using Models.Users;

namespace BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthenticationRequest, ApplicationUser>()
                .ForSourceMember(authRequest => authRequest.Password, options => options.DoNotValidate());

            CreateMap<UserCreateDto, ApplicationUser>()
                .ForSourceMember(user => user.Password, options => options.DoNotValidate());

            CreateMap<ApplicationUser, UserGetAllDto>();

            CreateMap<ApplicationUser, UserGetByIdDto>();
        }
    }
}
