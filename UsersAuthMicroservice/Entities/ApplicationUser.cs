using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public bool IsAdmin { get; set; }

        public virtual ICollection<ModeratorUserRole> ModeratorRoles { get; set; }
    }
}
