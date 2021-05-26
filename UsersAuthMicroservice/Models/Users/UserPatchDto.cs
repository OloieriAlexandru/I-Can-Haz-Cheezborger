using System;

namespace Models.Users
{
    public class UserPatchDto
    {
        public Guid Id { get; set; }

        public string Image { get; set; }
    }
}
