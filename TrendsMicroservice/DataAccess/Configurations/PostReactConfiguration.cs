using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DataAccess.Configurations
{
    public class PostReactConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<PostReact>
    {
        void IEntityTypeConfiguration<PostReact>.Configure(EntityTypeBuilder<PostReact> builder)
        {
            ConfigureBaseEntity(builder);

            builder.Property(pr => pr.PostId)
                .IsRequired();

            builder.Property(pr => pr.UserId)
                .IsRequired();

            builder.HasOne(pr => pr.Post)
                .WithMany(p => p.Reacts);

            builder.Property(pr => pr.Type)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ReactType)Enum.Parse(typeof(ReactType), v));

            builder.HasIndex(pr => new { pr.UserId, pr.PostId });
        }
    }
}
