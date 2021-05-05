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

        private readonly IAuthService authService;

        public PostService(HttpServiceResolver httpServiceResolver, IAuthService authService)
        {
            httpService = httpServiceResolver("trends");

            this.authService = authService;
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
            string url = $"api/v1/trends/{trendId}/posts/{postId}";
            await authService.Initialize();
            if (authService.IsLoggedIn())
            {
                url += "/auth";
            }
            return await httpService.Get<PostGetByIdDto>(url);
        }

        async Task<ICollection<PostGetAllDto>> IPostService.GetPosts(Guid trendId)
        {
            string url = $"api/v1/trends/{trendId}/posts";
            await authService.Initialize();
            if (authService.IsLoggedIn())
            {
                url += "/auth";
            }
            return await httpService.Get<PostGetAllDto[]>(url);
        }

        async Task IPostService.PatchPost(Guid trendId, Guid postId, PostPatchDto postPatchDto)
        {
            await httpService.Patch<ValueTask>($"api/v1/trends/{trendId}/posts/{postId}", postPatchDto);
        }

        async Task IPostService.PatchPostReact(Guid trendId, PostPatchReactDto postPatchReactDto)
        {
            await httpService.Patch<ValueTask>($"api/v1/trends/{trendId}/posts/{postPatchReactDto.Id}/react", postPatchReactDto);
        }
    }
}
