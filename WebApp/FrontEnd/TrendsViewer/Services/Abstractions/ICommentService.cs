using Models.Comments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentGetDto>> GetComments(Guid trendId, Guid postId);

        Task<CommentGetDto> GetComment(Guid trendId, Guid postId, Guid commentId);

        Task<CommentGetDto> CreateComment(Guid trendId, Guid postId, CommentCreateDto newComment);

        Task PatchCommentReact(Guid trendId, Guid postId, CommentPatchReactDto commentPatchReactDto);

        Task UpdateComment(Guid trendId, Guid postId, Guid commentId, CommentPatchDto commentPatchDto);
        
        Task DeleteComment(Guid trendId, Guid postId, Guid commentId);
    }
}
