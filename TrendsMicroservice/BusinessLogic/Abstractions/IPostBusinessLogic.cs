using Models.Posts;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface IPostBusinessLogic
    {
        ICollection<PostGetAllDto> GetAll(Guid trendId);

        PostGetByIdDto GetById(Guid id);

        PostGetAllDto Create(PostCreateDto post, string username);
        
        void Update(PostUpdateDto post);

        void Delete(Guid id);
    }
}
