using System;

namespace Models.Comments
{
    public class CommentPatchDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }
    }
}
