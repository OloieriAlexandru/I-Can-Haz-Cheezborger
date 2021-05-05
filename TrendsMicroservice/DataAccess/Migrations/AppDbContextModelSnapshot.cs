﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatorUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Downvotes")
                        .HasColumnType("int");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Upvotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Entities.CommentReact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId", "CommentId");

                    b.ToTable("CommentReact");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("int");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatorUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Downvotes")
                        .HasColumnType("int");

                    b.Property<string>("MediaPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TrendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Upvotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrendId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Entities.PostReact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId", "PostId");

                    b.ToTable("PostReact");
                });

            modelBuilder.Entity("Entities.Trend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatorUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Trends");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "Why so serious",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557376304.186_U5U7u5_100x100wp.webp",
                            Name = "Funny"
                        },
                        new
                        {
                            Id = new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "Just random things. Be nice.",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1481541784.8502_e8ARAR_100x100wp.webp",
                            Name = "Random"
                        },
                        new
                        {
                            Id = new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "We don't die, we respawn!",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286928.6604_uTYgug_100x100wp.webp",
                            Name = "Gaming"
                        },
                        new
                        {
                            Id = new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "It's so fluffy I'm gonna die!",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557391851.3248_Za4UdA_100x100wp.webp",
                            Name = "Animals"
                        },
                        new
                        {
                            Id = new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "Vroom vroom!",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557311278.4297_UNEHy6_100x100wp.webp",
                            Name = "Car"
                        },
                        new
                        {
                            Id = new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "The sports fanatics hub",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286774.0983_eGARyH_100x100wp.webp",
                            Name = "Sport"
                        },
                        new
                        {
                            Id = new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"),
                            CreatorId = new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"),
                            CreatorUsername = "admin",
                            Description = "Jaw-dropping moments",
                            ImageUrl = "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557310702.1267_UgysAp_100x100wp.webp",
                            Name = "WTF"
                        });
                });

            modelBuilder.Entity("Entities.TrendFollow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TrendId");

                    b.HasIndex("UserId", "TrendId");

                    b.ToTable("TrendFollow");
                });

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.HasOne("Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Entities.CommentReact", b =>
                {
                    b.HasOne("Entities.Comment", "Comment")
                        .WithMany("Reacts")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.HasOne("Entities.Trend", "Trend")
                        .WithMany("Posts")
                        .HasForeignKey("TrendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trend");
                });

            modelBuilder.Entity("Entities.PostReact", b =>
                {
                    b.HasOne("Entities.Post", "Post")
                        .WithMany("Reacts")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Entities.TrendFollow", b =>
                {
                    b.HasOne("Entities.Trend", "Trend")
                        .WithMany("Follows")
                        .HasForeignKey("TrendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Trend");
                });

            modelBuilder.Entity("Entities.Comment", b =>
                {
                    b.Navigation("Reacts");
                });

            modelBuilder.Entity("Entities.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reacts");
                });

            modelBuilder.Entity("Entities.Trend", b =>
                {
                    b.Navigation("Follows");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
