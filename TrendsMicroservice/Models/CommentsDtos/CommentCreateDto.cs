using System;

namespace Models
{
    public class CommentCreateDto
    {
        public string Text { get; set; }

        public Guid PostId { get; set; }
    }
}
