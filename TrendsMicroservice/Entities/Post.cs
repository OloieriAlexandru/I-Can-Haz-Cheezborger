using System;
using System.Collections.Generic;

namespace Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }
        
        public Guid TrendId { get; set; }
        
        public Trend Trend { get; set; }

        public string Username { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
