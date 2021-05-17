using DataAccess.Seed;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;

        public ApplicationUserConfiguration(IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        void IEntityTypeConfiguration<ApplicationUser>.Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(ur => ur.ModeratorRoles)
                .WithOne(mr => mr.User)
                .HasForeignKey(mr => mr.UserId)
                .IsRequired();

            builder.HasData(UsersSeed.Seed(passwordHasher));
        }
    }
}
