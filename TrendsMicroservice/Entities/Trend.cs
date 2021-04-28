using System.Collections.Generic;

namespace Entities
{
    public class Trend : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Username { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
