using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class TrendFollowConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<TrendFollow>
    {
        void IEntityTypeConfiguration<TrendFollow>.Configure(EntityTypeBuilder<TrendFollow> builder)
        {
            ConfigureBaseEntity(builder);

            builder.HasOne(tf => tf.Trend)
                .WithMany(t => t.Follows);

            builder.Property(tf => tf.UserId)
                .IsRequired();

            builder.Property(tf => tf.TrendId)
                .IsRequired();

            builder.HasIndex(tf => new { tf.UserId, tf.TrendId });
        }
    }
}
