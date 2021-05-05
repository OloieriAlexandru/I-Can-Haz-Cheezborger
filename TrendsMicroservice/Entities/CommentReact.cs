using System;

namespace Entities
{
    public class CommentReact : BaseEntity
    {
        public Guid CommentId { get; set; }

        public Comment Comment { get; set; }

        public Guid UserId { get; set; }

        public ReactType Type { get; set; }
    }
}
