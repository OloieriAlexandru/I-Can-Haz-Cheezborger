using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace TrendsViewer.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentGetDto>> GetComments(Guid idTrend, Guid idPost);

        Task<CommentGetDto> GetComment(Guid trendId, Guid postId, Guid commentId);

        Task<CommentGetDto> CreateComment(Guid idTrend, Guid idPost, CommentCreateDto newComment);

        Task UpdateComment(Guid idTrend, Guid idPost, Guid idComment, CommentUpdateDto updatedComment);
        
        Task DeleteComment(Guid idTrend, Guid idPost, Guid idComment);
    }
}
