using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<CommentDto>> GetComments(Guid idTrend, Guid idPost)
        {
            return await httpClient.GetJsonAsync<CommentDto[]>($"api/v1/trends/{idTrend}/posts/{idPost}/comments");
        }

        public async Task<CommentDto> GetComment(Guid idTrend, Guid idPost, Guid idComment)
        {
            return await httpClient.GetJsonAsync<CommentDto>($"api/v1/trends/{idTrend}/posts/{idPost}/comments/{idComment}");
        }

        public Task<CommentDto> UpdateComment(Guid idTrend, Guid idPost, Guid idComment, CommentDto updatedComment)
        {
            return (Task<CommentDto>)httpClient.PutJsonAsync($"api/v1/trends/{idTrend}/posts/{idPost}/comments/{idComment}", updatedComment);
        }

        public async Task<CommentDto> CreateComment(Guid idTrend, Guid idPost, CommentDto newComment)
        {
            return await httpClient.PostJsonAsync<CommentDto>($"api/v1/trends/{idTrend}/posts/{idPost}/comments", newComment);
        }

        public async Task DeleteComment(Guid idTrend, Guid idPost, Guid idComment)
        {
            await httpClient.DeleteAsync($"api/v1/trends/{idTrend}/posts/{idPost}/comments/{idComment}");
        }
    }
}
