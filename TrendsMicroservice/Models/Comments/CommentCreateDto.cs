using System;

namespace Models.Comments
{
    public class CommentCreateDto
    {
        public string Text { get; set; }

        public Guid PostId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CommentGetDto dto &&
                   Text == dto.Text &&
                   PostId == dto.PostId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text, PostId);
        }
    }
}
