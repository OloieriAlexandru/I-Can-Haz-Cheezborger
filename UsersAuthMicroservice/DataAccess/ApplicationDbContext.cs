using DataAccess.Configurations;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessLogic
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ModeratorRole, Guid,
            IdentityUserClaim<Guid>, ModeratorUserRole, IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IPasswordHasher<ApplicationUser> passwordHasher) : base(options)
        {
            this.passwordHasher = passwordHasher;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration(passwordHasher));

            builder.ApplyConfiguration(new ModeratorRoleConfiguration());
        }
    }
}
