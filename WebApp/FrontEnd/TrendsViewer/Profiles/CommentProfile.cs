using AutoMapper;
using Models.Comments;
using TrendsViewer.FormModels;

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
