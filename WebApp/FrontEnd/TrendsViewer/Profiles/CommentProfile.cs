using AutoMapper;
using Models.Comments;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentUpdateDto, EditCommentModel>();
            CreateMap<EditCommentModel, CommentUpdateDto>();
        }
       
    }
}
