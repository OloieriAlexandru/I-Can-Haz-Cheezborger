using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.Auth;
using Models.Users;

namespace BusinessLogic.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthenticationRequest, IdentityUser>()
                .ForSourceMember(authRequest => authRequest.Password, options => options.DoNotValidate());

            CreateMap<UserCreateDto, IdentityUser>()
                .ForSourceMember(user => user.Password, options => options.DoNotValidate());

            CreateMap<IdentityUser, UserGetAllDto>();
        }
    }
}
