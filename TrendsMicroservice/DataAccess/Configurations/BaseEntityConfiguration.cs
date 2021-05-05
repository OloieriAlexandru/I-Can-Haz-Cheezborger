using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public abstract class BaseEntityConfiguration
    {
        protected BaseEntityConfiguration()
        {
        }

        public static void ConfigureBaseEntity<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();
        }
    }
}
