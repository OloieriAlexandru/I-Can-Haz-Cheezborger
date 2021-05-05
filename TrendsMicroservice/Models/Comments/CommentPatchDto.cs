using System;

namespace Models.Comments
{
    public class CommentPatchDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
