using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class CommentEntityTypeConfiguration : BaseEntityTypeConfiguration, IEntityTypeConfiguration<Comment>
    {
        void IEntityTypeConfiguration<Comment>.Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Text)
                .IsRequired();

            builder.Property(c => c.Upvotes)
                .IsRequired();

            builder.Property(c => c.Downvotes)
                .IsRequired();

            builder.Property(c => c.PostId)
                .IsRequired();

            builder.Property(c => c.Username)
                .IsRequired();

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments);
        }
    }
}
