using System.Collections.Generic;

namespace Entities
{
    public class Trend : UserCreatedEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool ApprovedImage { get; set; }

        public bool ApprovedText { get; set; }
        public int FollowersCount { get; set; }

        public int PostsCount { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<TrendFollow> Follows { get; set; }
    }
}
