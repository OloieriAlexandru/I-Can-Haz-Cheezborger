using System;

namespace Models
{
    public class PostGetAllDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string MediaPath { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public Guid TrendId { get; set; }
    }
}
