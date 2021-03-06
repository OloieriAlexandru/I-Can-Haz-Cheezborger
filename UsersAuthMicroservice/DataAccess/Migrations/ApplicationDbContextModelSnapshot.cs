// <auto-generated />
using System;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7cec0a00-a9b7-43a3-bfc4-f4778eb80f39"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "812ed584-483d-41bc-ae5a-f86827d3ac9a",
                            Email = "admin@icanhaz.com",
                            EmailConfirmed = false,
                            IsAdmin = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "admin@icanhaz.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEN48MgAB79/qQ0ns/aiaNFmA7YzL4apcHJm8It8agW36Om7AL6TGlWiDuwK64ra8Yg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "94f55673-a9ba-4e6b-ac84-2598031ce347",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("da3ff9e7-2647-457f-9b62-0ff9ab3177ca"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1f98777f-4d74-4206-b459-1e8e409b0e56",
                            Email = "olo@icanhaz.com",
                            EmailConfirmed = false,
                            IsAdmin = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "olo@icanhaz.com",
                            NormalizedUserName = "olo",
                            PasswordHash = "AQAAAAEAACcQAAAAEFuGV+60i+H0aiUAd0r0DzH3f7jeA9noe6jUDX2sYe6+XrtqztM488nQi/ndNFlq1g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8c787c1c-1e57-481f-996e-e03b5ac3b9fc",
                            TwoFactorEnabled = false,
                            UserName = "olo"
                        },
                        new
                        {
                            Id = new Guid("b706a1dd-8e1b-4a26-b1f4-0681e82c59d3"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a0f0dfe1-a020-4a85-988b-ceb3d85193f5",
                            Email = "ramona@icanhaz.com",
                            EmailConfirmed = false,
                            IsAdmin = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ramona@icanhaz.com",
                            NormalizedUserName = "ramona",
                            PasswordHash = "AQAAAAEAACcQAAAAEObP+aspBzeZbxzlk+BXrQ9wkLJ6bWoDKN0lUTMnfHZ0vrlfgydHOQ26quZhiU+BKg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8fdb2aa7-b197-423b-94da-1205590f4c8d",
                            TwoFactorEnabled = false,
                            UserName = "ramona"
                        },
                        new
                        {
                            Id = new Guid("3e0294ec-2b76-4ae8-ad4a-3b55e64fceeb"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bd4f86b1-b1b9-4a09-b852-bc0a07621f1d",
                            Email = "alex@icanhaz.com",
                            EmailConfirmed = false,
                            IsAdmin = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "alex@icanhaz.com",
                            NormalizedUserName = "alex",
                            PasswordHash = "AQAAAAEAACcQAAAAEN083F681I2B1aqzbFjgUm8A1pcafvxUreEW8uLljf5vSS/f/uk/99sCHqiiOPkIqw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "df0af0f7-cf54-4ef5-80ed-0e08f5839f7f",
                            TwoFactorEnabled = false,
                            UserName = "alex"
                        },
                        new
                        {
                            Id = new Guid("ad380f15-f443-4098-a88b-70347fe6b4e5"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "40ec09a3-5ba7-49ba-8abb-a603da41f2a1",
                            Email = "andy@icanhaz.com",
                            EmailConfirmed = false,
                            IsAdmin = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "andy@icanhaz.com",
                            NormalizedUserName = "andy",
                            PasswordHash = "AQAAAAEAACcQAAAAELmuZRgW/tVhvaL75aEqxv60VPvCdFqzRDJZQie8gSpGoTu0HXNRsyImaKJoBSKehw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f471fb83-1df7-466d-976a-5cbe2fb5e16e",
                            TwoFactorEnabled = false,
                            UserName = "andy"
                        });
                });

            modelBuilder.Entity("Entities.ModeratorRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Entities.ModeratorUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Entities.ModeratorUserRole", b =>
                {
                    b.HasOne("Entities.ModeratorRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.ApplicationUser", "User")
                        .WithMany("ModeratorRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Entities.ModeratorRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.ApplicationUser", b =>
                {
                    b.Navigation("ModeratorRoles");
                });

            modelBuilder.Entity("Entities.ModeratorRole", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
