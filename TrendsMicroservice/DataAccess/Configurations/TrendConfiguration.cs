using Common.Constraints;
using DataAccess.Seed;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class TrendConfiguration : UserCreatedEntityConfiguration, IEntityTypeConfiguration<Trend>
    {
        void IEntityTypeConfiguration<Trend>.Configure(EntityTypeBuilder<Trend> builder)
        {
            ConfigureUserCreatedEntity(builder);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(TrendConstraints.NameMaxLength);

            builder.Property(t => t.FollowersCount)
                .IsRequired();

            builder.Property(t => t.PostsCount)
                .IsRequired();

            builder.HasData(TrendsSeed.Seed());
        }
    }
}
