using System;
using System.Collections.Generic;

namespace Models
{
    public class PostDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string MediaPath { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public Guid? TrendId { get; set; }
        public TrendDto TrendDto { get; set; }
        public ICollection<CommentDto> CommentsDtos { get; set; }
    }
}
