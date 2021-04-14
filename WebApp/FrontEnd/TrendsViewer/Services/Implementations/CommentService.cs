using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Models;
using TrendsViewer.Services.Abstractions;

namespace TrendsViewer.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient httpClient;

        public CommentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        async Task<CommentGetDto> ICommentService.CreateComment(Guid trendId, Guid postId, CommentCreateDto newComment)
        {
            return await httpClient.PostJsonAsync<CommentGetDto>($"api/v1/trends/{trendId}/posts/{postId}/comments", newComment);
        }

        async Task ICommentService.DeleteComment(Guid trendId, Guid postId, Guid commentId)
        {
            await httpClient.DeleteAsync($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentId}");
        }

        async Task<CommentGetDto> ICommentService.GetComment(Guid trendId, Guid postId, Guid commentId)
        {
            return await httpClient.GetJsonAsync<CommentGetDto>($"api/v1/trends/{trendId}/posts/{postId}/comments/{commentId}");
        }

        async Task<IEnumerable<CommentGetDto>> ICommentService.GetComments(Guid trendId, Guid postId)
        {
            return await httpClient.GetJsonAsync<CommentGetDto[]>($"api/v1/trends/{trendId}/posts/{postId}/comments");
        }

        async Task ICommentService.UpdateComment(Guid trendId, Guid postId, Guid commentId, CommentUpdateDto updatedComment)
        {
            await httpClient.PutJsonAsync($"api/v1/trends/{trendId}/posts/{postId}/{updatedComment.Id}", updatedComment);
        }
    }
}
