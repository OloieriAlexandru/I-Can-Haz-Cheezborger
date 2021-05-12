using System.Collections.Generic;

namespace Entities
{
    public class Trend : UserCreatedEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<TrendFollow> Follows { get; set; }
    }
}
