﻿using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
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

        async Task<PostGetAllDto> IPostService.CreatePost(Guid trendId, PostCreateDto newPost)
        {
            return await httpClient.PostJsonAsync<PostGetAllDto>($"api/v1/trends/{trendId}/posts", newPost);
        }

        async Task IPostService.DeletePost(Guid trendId, Guid postId)
        {
            await httpClient.DeleteAsync($"api/v1/trends/{trendId}/posts/{postId}");
        }

        async Task<PostGetByIdDto> IPostService.GetPost(Guid trendId, Guid postId)
        {
            return await httpClient.GetJsonAsync<PostGetByIdDto>($"api/v1/trends/{trendId}/posts/{postId}");
        }

        async Task<IEnumerable<PostGetAllDto>> IPostService.GetPosts(Guid trendId)
        {
            return await httpClient.GetJsonAsync<PostGetAllDto[]>($"api/v1/trends/{trendId}");
        }

        async Task IPostService.UpdatePost(Guid trendId, Guid postId, PostUpdateDto updatedPost)
        {
            await httpClient.PutJsonAsync($"api/v1/trends/{trendId}/posts/{postId}", updatedPost);
        }
    }
}
