using System;

namespace Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public string Username { get; set; }

        public Guid PostId { get; set; }

        public Post Post { get; set; }
    }
}
