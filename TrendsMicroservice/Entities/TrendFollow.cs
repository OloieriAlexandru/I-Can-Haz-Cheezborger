using System;

namespace Entities
{
    public class TrendFollow : BaseEntity
    {
        public Guid TrendId { get; set; }

        public Trend Trend { get; set; }

        public Guid UserId { get; set; }
    }
}
