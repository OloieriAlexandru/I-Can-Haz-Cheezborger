using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserLoginDto, RegisterUserModel>()
                .ForMember(dest => dest.PasswordCheck,
                           opt => opt.MapFrom(src => src.Password));
            CreateMap<RegisterUserModel, UserLoginDto>();
        }
    }
}
