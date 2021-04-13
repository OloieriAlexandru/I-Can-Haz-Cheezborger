using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendsViewer.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetPosts(Guid trendId);
        Task<PostDto> GetPost(Guid trendId, Guid postId);
        Task<PostDto> UpdatePost(Guid trendId, Guid postId, PostDto updatedPost);
        Task<PostDto> CreatePost(Guid trendId, PostDto newPost);
        Task DeletePost(Guid trendId, Guid postId);
    }
}
