using Models.Comments;
using Models.Common;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Abstractions
{
    public interface ICommentBusinessLogic
    {
        ICollection<CommentGetDto> GetAll(Guid postId);

        CommentGetDto GetById(Guid id);
        
        CommentGetDto Create(CommentCreateDto comment);
        
        void Patch(CommentPatchDto comment);

        void PatchReact(CommentPatchReactDto commentPatchReact);

        void PatchContentScanTaskApprovals(Guid id, PatchContentScanTaskApprovalsDto taskApprovalsDto);

        void Delete(Guid id);
    }
}
