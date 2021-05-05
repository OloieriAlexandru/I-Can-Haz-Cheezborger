using System;

namespace Entities
{
    public class PostReact : BaseEntity
    {
        public Guid PostId { get; set; }

        public Post Post { get; set; }

        public Guid UserId { get; set; }

        public ReactType Type { get; set; }
    }
}
