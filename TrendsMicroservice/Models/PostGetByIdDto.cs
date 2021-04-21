using System;
using System.Collections.Generic;

namespace Models
{
    public class PostGetByIdDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid TrendId { get; set; }

        public ICollection<CommentGetDto> Comments { get; set; }

        public Boolean LikeClicked { get; set; } = false;

        public Boolean DislikeClicked { get; set; } = false;

        public override bool Equals(object obj)
        {
            return obj is PostGetByIdDto dto &&
                   Id == dto.Id &&
                   Title == dto.Title &&
                   MediaPath == dto.MediaPath &&
                   TrendId == dto.TrendId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, MediaPath, TrendId);
        }
    }
}
