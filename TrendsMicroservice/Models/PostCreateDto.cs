using System;

namespace Models
{
    public class PostCreateDto
    {
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public Guid TrendId { get; set; }
    }
}
