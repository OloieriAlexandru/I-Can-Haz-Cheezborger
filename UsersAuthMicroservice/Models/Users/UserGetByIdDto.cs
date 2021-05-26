using System;
using System.Collections.Generic;

namespace Models.Users
{
    public class UserGetByIdDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Guid> ModeratedTrendsIds { get; set; }
    }
}
