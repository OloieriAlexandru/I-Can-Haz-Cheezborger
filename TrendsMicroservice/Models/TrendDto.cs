using System;
using System.Collections.Generic;

namespace Models
{
    public class TrendDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public ICollection<PostDto> PostsDtos { get; set; }
    }
}
