using AutoMapper;
using Entities;
using Models;

namespace BusinessLogic.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentGetDto>();

            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
        }
    }
}
