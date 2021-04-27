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
        
        Task UpdatePost(Guid trendId, Guid postId, PostUpdateDto updatedPost);
        
        Task<PostGetAllDto> CreatePost(Guid trendId, PostCreateDto newPost);
        
        Task DeletePost(Guid trendId, Guid postId);
    }
}
