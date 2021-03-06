using AutoMapper;
using Entities;
using Models.Comments;

namespace BusinessLogic.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentGetDto>();

            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentPatchDto, Comment>();
        }
    }
}
