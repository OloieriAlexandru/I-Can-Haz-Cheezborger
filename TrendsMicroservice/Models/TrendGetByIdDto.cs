using System.Collections.Generic;

namespace Models
{
    public class TrendGetByIdDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<PostGetAllDto> Posts { get; set; }
    }
}
