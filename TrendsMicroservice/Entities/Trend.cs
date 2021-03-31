using System.Collections.Generic;

namespace Entities
{
    public class Trend : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
