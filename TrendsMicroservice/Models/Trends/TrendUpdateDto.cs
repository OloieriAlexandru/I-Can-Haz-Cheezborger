using System;

namespace Models.Trends
{
    public class TrendUpdateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string DateTime { get; set; }
    }
}
