using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Configurations
{
    public class CommentReactConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<CommentReact>
    {
        void IEntityTypeConfiguration<CommentReact>.Configure(EntityTypeBuilder<CommentReact> builder)
        {
            ConfigureBaseEntity(builder);

            builder.HasOne(cr => cr.Comment)
                .WithMany(c => c.Reacts);

            builder.Property(cr => cr.CommentId)
                .IsRequired();

            builder.Property(cr => cr.UserId)
                .IsRequired();

            builder.Property(cr => cr.Type)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ReactType)Enum.Parse(typeof(ReactType), v));

            builder.HasIndex(cr => new { cr.UserId, cr.CommentId });
        }
    }
}
