using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface IPostBusinessLogic
    {
        ICollection<PostDto> GetAll();
        PostDto GetById(Guid id);
        void Create(PostDto post);
        void Update(PostDto post);
    }
}
