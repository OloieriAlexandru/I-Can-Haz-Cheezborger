using Models.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendsViewer.Services.Abstractions;
using TrendsViewer.Services.Resolvers;

namespace TrendsViewer.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IHttpService httpService;

        public PostService(HttpServiceResolver httpServiceResolver)
        {
            httpService = httpServiceResolver("trends");
        }

        async Task<PostGetAllDto> IPostService.CreatePost(Guid trendId, PostCreateDto newPost)
        {
            return await httpService.Post<PostGetAllDto>($"api/v1/trends/{trendId}/posts", newPost);
        }

        async Task IPostService.DeletePost(Guid trendId, Guid postId)
        {
            await httpService.Delete<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}");
        }

        async Task<PostGetByIdDto> IPostService.GetPost(Guid trendId, Guid postId)
        {
            return await httpService.Get<PostGetByIdDto>($"api/v1/trends/{trendId}/posts/{postId}");
        }

        async Task<ICollection<PostGetAllDto>> IPostService.GetPosts(Guid trendId)
        {
            return await httpService.Get<PostGetAllDto[]>($"api/v1/trends/{trendId}/posts");
        }

        async Task IPostService.UpdatePost(Guid trendId, Guid postId, PostUpdateDto updatedPost)
        {
            await httpService.Put<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}", updatedPost);
        }
    }
}
