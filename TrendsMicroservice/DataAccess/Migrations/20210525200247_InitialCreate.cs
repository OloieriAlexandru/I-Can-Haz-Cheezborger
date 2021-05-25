using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedImage = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedText = table.Column<bool>(type: "bit", nullable: false),
                    FollowersCount = table.Column<int>(type: "int", nullable: false),
                    PostsCount = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    Downvotes = table.Column<int>(type: "int", nullable: false),
                    ApprovedImage = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedText = table.Column<bool>(type: "bit", nullable: false),
                    TrendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentsCount = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Trends_TrendId",
                        column: x => x.TrendId,
                        principalTable: "Trends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrendFollows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrendFollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrendFollows_Trends_TrendId",
                        column: x => x.TrendId,
                        principalTable: "Trends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    Downvotes = table.Column<int>(type: "int", nullable: false),
                    ApprovedImage = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedText = table.Column<bool>(type: "bit", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorUsername = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReact_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentReact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReact_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trends",
                columns: new[] { "Id", "ApprovedImage", "ApprovedText", "CreatorId", "CreatorUsername", "Description", "FollowersCount", "ImageUrl", "Name", "PostsCount" },
                values: new object[,]
                {
                    { new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "Why so serious", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557376304.186_U5U7u5_100x100wp.webp", "Funny", 0 },
                    { new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "Just random things. Be nice.", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1481541784.8502_e8ARAR_100x100wp.webp", "Random", 0 },
                    { new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "We don't die, we respawn!", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286928.6604_uTYgug_100x100wp.webp", "Gaming", 0 },
                    { new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "It's so fluffy I'm gonna die!", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557391851.3248_Za4UdA_100x100wp.webp", "Animals", 0 },
                    { new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "Vroom vroom!", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557311278.4297_UNEHy6_100x100wp.webp", "Car", 0 },
                    { new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "The sports fanatics hub", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557286774.0983_eGARyH_100x100wp.webp", "Sport", 0 },
                    { new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"), true, true, new Guid("e03f1453-9194-47e8-83c4-9eac442f216d"), "admin", "Jaw-dropping moments", 0, "https://miscmedia-9gag-fun.9cache.com/images/thumbnail-facebook/1557310702.1267_UgysAp_100x100wp.webp", "WTF", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReact_CommentId",
                table: "CommentReact",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReact_UserId_CommentId",
                table: "CommentReact",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReact_PostId",
                table: "PostReact",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReact_UserId_PostId",
                table: "PostReact",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TrendId",
                table: "Posts",
                column: "TrendId");

            migrationBuilder.CreateIndex(
                name: "IX_TrendFollows_TrendId",
                table: "TrendFollows",
                column: "TrendId");

            migrationBuilder.CreateIndex(
                name: "IX_TrendFollows_UserId_TrendId",
                table: "TrendFollows",
                columns: new[] { "UserId", "TrendId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReact");

            migrationBuilder.DropTable(
                name: "PostReact");

            migrationBuilder.DropTable(
                name: "TrendFollows");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Trends");
        }
    }
}
