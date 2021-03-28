using Common.Constraints;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class TrendEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Trend>
    {
        void IEntityTypeConfiguration<Trend>.Configure(EntityTypeBuilder<Trend> builder)
        {
            Configure(builder);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(TrendConstraints.NameMaxLength);
        }
    }
}
