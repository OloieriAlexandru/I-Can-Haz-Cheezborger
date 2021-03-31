using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public abstract class BaseEntityTypeConfiguration
    {
        protected BaseEntityTypeConfiguration()
        {
        }

        public static void Configure<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();
        }
    }
}
