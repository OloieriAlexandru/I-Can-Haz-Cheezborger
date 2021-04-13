using System;

namespace Models
{
    public class CommentGetDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid PostId { get; set; }
    }
}
