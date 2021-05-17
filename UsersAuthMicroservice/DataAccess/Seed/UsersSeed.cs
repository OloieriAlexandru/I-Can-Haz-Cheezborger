using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataAccess.Seed
{
    public static class UsersSeed
    {
        public static ICollection<ApplicationUser> Seed(IPasswordHasher<ApplicationUser> passwordHasher)
        {
            ICollection<ApplicationUser> users = new List<ApplicationUser>();
            AddUser(users, passwordHasher, Guid.Parse("7cec0a00-a9b7-43a3-bfc4-f4778eb80f39"), "admin@icanhaz.com", "admin", "94f55673-a9ba-4e6b-ac84-2598031ce347");
            AddUser(users, passwordHasher, Guid.Parse("da3ff9e7-2647-457f-9b62-0ff9ab3177ca"), "olo@icanhaz.com", "olo", "8c787c1c-1e57-481f-996e-e03b5ac3b9fc");
            AddUser(users, passwordHasher, Guid.Parse("b706a1dd-8e1b-4a26-b1f4-0681e82c59d3"), "ramona@icanhaz.com", "ramona", "8fdb2aa7-b197-423b-94da-1205590f4c8d");
            AddUser(users, passwordHasher, Guid.Parse("3e0294ec-2b76-4ae8-ad4a-3b55e64fceeb"), "alex@icanhaz.com", "alex", "df0af0f7-cf54-4ef5-80ed-0e08f5839f7f");
            AddUser(users, passwordHasher, Guid.Parse("ad380f15-f443-4098-a88b-70347fe6b4e5"), "andy@icanhaz.com", "andy", "f471fb83-1df7-466d-976a-5cbe2fb5e16e");
            return users;
        }

        private static void AddUser(ICollection<ApplicationUser> users, IPasswordHasher<ApplicationUser> passwordHasher, Guid id, string email, string username, string securityStamp)
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = id,
                Email = email,
                UserName = username,
                NormalizedEmail = email,
                NormalizedUserName = username,
                SecurityStamp = securityStamp,
                IsAdmin = true
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "@PPaarrolaaa123");
            users.Add(user);
        }
    }
}
