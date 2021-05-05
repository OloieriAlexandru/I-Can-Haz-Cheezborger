using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public abstract class UserCreatedEntityConfiguration : BaseEntityConfiguration
    {
        protected UserCreatedEntityConfiguration()
        {
        }

        public static void ConfigureUserCreatedEntity<T>(EntityTypeBuilder<T> builder) where T : UserCreatedEntity
        {
            ConfigureBaseEntity(builder);

            builder.Property(e => e.CreatorUsername)
                .IsRequired();

            builder.Property(e => e.CreatorId)
                .IsRequired();
        }
    }
}
