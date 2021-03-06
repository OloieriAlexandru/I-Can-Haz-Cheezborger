using System;

namespace Models.Posts
{
    public class PostCreateDto : UserInfoModel
    {
        public string Title { get; set; }

        public string MediaPath { get; set; }

        public string Description { get; set; }

        public Guid TrendId { get; set; }
    }
}
