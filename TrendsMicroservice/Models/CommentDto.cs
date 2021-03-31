using System;

namespace Models
{
    public class CommentDto
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public Guid? PostId { get; set; }
        public PostDto PostDto { get; set; }
    }
}
