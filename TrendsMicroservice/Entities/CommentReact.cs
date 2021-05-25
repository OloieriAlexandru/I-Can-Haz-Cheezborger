using System;

namespace Entities
{
    public class CommentReact : BaseEntity
    {
        public Guid CommentId { get; set; }

        public Comment Comment { get; set; }

        public Guid UserId { get; set; }

        public ReactType Type { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
