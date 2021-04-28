using Models.Comments;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ICommentBusinessLogic
    {
        ICollection<CommentGetDto> GetAll(Guid postId);

        CommentGetDto GetById(Guid id);
        
        CommentGetDto Create(CommentCreateDto comment, string username);
        
        void Update(CommentUpdateDto comment);

        void Delete(Guid id);
    }
}
