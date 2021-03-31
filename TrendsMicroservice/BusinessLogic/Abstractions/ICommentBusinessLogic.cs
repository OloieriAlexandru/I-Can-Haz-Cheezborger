using Models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ICommentBusinessLogic
    {
        ICollection<CommentDto> GetAll();
        CommentDto GetById(Guid id);
        void Create(CommentDto Comment);
        void Update(CommentDto Comment);
    }
}
