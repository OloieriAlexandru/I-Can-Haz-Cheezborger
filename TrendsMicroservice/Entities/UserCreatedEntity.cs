using System;

namespace Entities
{
    public class UserCreatedEntity : BaseEntity
    {
        public Guid CreatorId { get; set; }

        public string CreatorUsername { get; set; }
    }
}
