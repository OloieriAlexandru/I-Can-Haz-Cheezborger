using Models.Posts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrendsViewer.Services.Abstractions
{
    public interface IPostService
    {
        Task<ICollection<PostGetAllDto>> GetPosts(Guid trendId);

        Task<PostGetByIdDto> GetPost(Guid trendId, Guid postId);
        
        Task PatchPost(Guid trendId, Guid postId, PostPatchDto postPatchDto);

        Task PatchPostReact(Guid trendId, PostPatchReactDto postPatchReactDto);

        Task<PostGetAllDto> CreatePost(Guid trendId, PostCreateDto newPost);
        
        Task DeletePost(Guid trendId, Guid postId);
    }
}
