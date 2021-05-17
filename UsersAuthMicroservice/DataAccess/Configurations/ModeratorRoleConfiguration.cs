using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ModeratorRoleConfiguration : IEntityTypeConfiguration<ModeratorRole>
    {
        void IEntityTypeConfiguration<ModeratorRole>.Configure(EntityTypeBuilder<ModeratorRole> builder)
        {
            builder.HasMany(mr => mr.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
