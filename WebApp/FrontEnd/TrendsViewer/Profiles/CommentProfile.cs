using AutoMapper;
using Models.Comments;
using TrendsViewer.Models;

namespace TrendsViewer.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentPatchDto, EditCommentModel>();
            CreateMap<EditCommentModel, CommentPatchDto>();
        }
       
    }
}
