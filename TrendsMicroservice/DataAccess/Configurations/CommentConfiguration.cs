using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Configurations
{
    public class CommentConfiguration : UserCreatedEntityConfiguration, IEntityTypeConfiguration<Comment>
    {
        void IEntityTypeConfiguration<Comment>.Configure(EntityTypeBuilder<Comment> builder)
        {
            ConfigureUserCreatedEntity(builder);

            builder.Property(c => c.Text)
                .IsRequired();

            builder.Property(c => c.Upvotes)
                .IsRequired();

            builder.Property(c => c.Downvotes)
                .IsRequired();

            builder.Property(c => c.PostId)
                .IsRequired();

            builder.Property(c => c.CreateDate)
                .IsRequired();

            builder.HasOne(c => c.Post)
                .WithMany(p => p.Comments);
        }
    }
}
