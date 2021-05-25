using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Resolvers;

namespace TrendsViewer.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IHttpService httpService;

        public CommentService(HttpServiceResolver httpServiceResolver)
        {
            httpService = httpServiceResolver("trends");
        }

        async Task<CommentGetDto> ICommentService.CreateComment(Guid trendId, Guid postId, CommentCreateDto newComment)
        {
            return await httpService.Post<CommentGetDto>($"api/v1/trends/{trendId}/posts/{postId}/comments", newComment);
        }

        async Task ICommentService.DeleteComment(Guid trendId, Guid postId, Guid commentId)
        {
            await httpService.Delete<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentId}");
        }

        async Task<CommentGetDto> ICommentService.GetComment(Guid trendId, Guid postId, Guid commentId)
        {
            return await httpService.Get<CommentGetDto>($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentId}");
        }

        async Task<IEnumerable<CommentGetDto>> ICommentService.GetComments(Guid trendId, Guid postId)
        {
            return await httpService.Get<CommentGetDto[]>($"api/v1/trends/{trendId}/posts/{postId}/comments");
        }

        async Task ICommentService.UpdateComment(Guid trendId, Guid postId, Guid commentId, CommentPatchDto commentPatchDto)
        {
            await httpService.Patch<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentPatchDto.Id}", commentPatchDto);
        }

        async Task ICommentService.PatchCommentReact(Guid trendId, Guid postId, CommentPatchReactDto commentPatchReactDto)
        {
            await httpService.Patch<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentPatchReactDto.Id}/react", commentPatchReactDto);
        }
    }
}
