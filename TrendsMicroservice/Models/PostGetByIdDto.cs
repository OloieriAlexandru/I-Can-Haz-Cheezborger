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
    }
}
