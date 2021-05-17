using System;

namespace Models.Users
{
    public class UserGetAllDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
