using System;

namespace Models.Comments
{
    public class CommentGetDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid PostId { get; set; }


        public override bool Equals(object obj)
        {
            return obj is CommentGetDto dto &&
                   Id == dto.Id &&
                   Text == dto.Text &&
                   PostId == dto.PostId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text, PostId);
        }
    }
}