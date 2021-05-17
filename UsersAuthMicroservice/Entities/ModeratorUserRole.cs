using Microsoft.AspNetCore.Identity;
using System;

namespace Entities
{
    public class ModeratorUserRole : IdentityUserRole<Guid>
    {
        public virtual ApplicationUser User { get; set; }

        public virtual ModeratorRole Role { get; set; }
    }
}
