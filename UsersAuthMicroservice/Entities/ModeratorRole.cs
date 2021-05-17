using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class ModeratorRole : IdentityRole<Guid>
    {
        public virtual ICollection<ModeratorUserRole> UserRoles { get; set; }
    }
}
