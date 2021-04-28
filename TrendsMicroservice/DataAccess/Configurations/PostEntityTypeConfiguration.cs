using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class PostEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Post>
    {
       void IEntityTypeConfiguration<Post>.Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.MediaPath)
                .IsRequired();

            builder.Property(p => p.Upvotes)
                .IsRequired();

            builder.Property(p => p.Downvotes)
                .IsRequired();

            builder.Property(p => p.Username)
                .IsRequired();

            builder.HasOne(p => p.Trend)
                .WithMany(t => t.Posts);
        }
    }
}
