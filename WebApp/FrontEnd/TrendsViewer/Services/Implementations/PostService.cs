using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TrendsViewer.Services
{
    public class PostService : IPostService
    {
        private readonly HttpClient httpClient;
        public PostService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> GetPosts(Guid trendId)
        {
            string api = $"api/v1/trends/{trendId}";
            return await httpClient.GetJsonAsync<PostDto[]>(api);
        }

        public async Task<PostDto> GetPost(Guid trendId, Guid postId)
        {
            string api = $"api/v1/trends/{trendId}/posts/{postId}";
            return await httpClient.GetJsonAsync<PostDto>(api);
        }

        public Task<PostDto> UpdatePost(Guid trendId, Guid postId, PostDto updatedPost)
        {
            string api = $"api/v1/trends/{trendId}/posts/{postId}";
            return httpClient.PutJsonAsync<PostDto>(api, updatedPost);
        }


        public async Task<PostDto> CreatePost(Guid trendId, PostDto newPost)
        {
            string api = $"api/v1/trends/{trendId}/posts";
            return await httpClient.PostJsonAsync<PostDto>(api, newPost);
        }

        public async Task DeletePost(Guid trendId, Guid postId)
        {
            string api = $"api/v1/trends/{trendId}/posts/{postId}";
            await httpClient.DeleteAsync(api);
        }
    }
}
