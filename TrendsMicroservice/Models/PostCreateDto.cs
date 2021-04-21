using System;

namespace Models
{
    public class PostCreateDto
    {
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public Guid TrendId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PostCreateDto dto &&
                   Title == dto.Title &&
                   MediaPath == dto.MediaPath &&
                   TrendId == dto.TrendId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, MediaPath, TrendId);
        }
    }

}
