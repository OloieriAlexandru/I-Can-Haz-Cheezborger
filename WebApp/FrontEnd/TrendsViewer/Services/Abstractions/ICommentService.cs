using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace TrendsViewer.Services.Abstractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetComments(Guid idTrend, Guid idPost);
        Task<CommentDto> GetComment(Guid idTrend, Guid idPost, Guid idComment);
        Task<CommentDto> UpdateComment(Guid idTrend, Guid idPost, Guid idComment, CommentDto updatedComment);
        Task<CommentDto> CreateComment(Guid idTrend, Guid idPost, CommentDto newComment);
        Task DeleteComment(Guid idTrend, Guid idPost, Guid idComment);
    }
}
