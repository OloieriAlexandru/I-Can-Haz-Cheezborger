using System;
using System.Collections.Generic;

namespace Entities
{
    public class Post : UserCreatedEntity
    {
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }

        public Guid TrendId { get; set; }
        
        public DateTime CreateDate { get; set; }

        public Trend Trend { get; set; }

        public int CommentsCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PostReact> Reacts { get; set; }

        public Post()
        {
            CreateDate = DateTime.Now;
        }
    }
}
