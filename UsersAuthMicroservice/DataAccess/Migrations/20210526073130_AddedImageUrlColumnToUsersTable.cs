using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedImageUrlColumnToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3e0294ec-2b76-4ae8-ad4a-3b55e64fceeb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bd4f86b1-b1b9-4a09-b852-bc0a07621f1d", "AQAAAAEAACcQAAAAEN083F681I2B1aqzbFjgUm8A1pcafvxUreEW8uLljf5vSS/f/uk/99sCHqiiOPkIqw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7cec0a00-a9b7-43a3-bfc4-f4778eb80f39"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "812ed584-483d-41bc-ae5a-f86827d3ac9a", "AQAAAAEAACcQAAAAEN48MgAB79/qQ0ns/aiaNFmA7YzL4apcHJm8It8agW36Om7AL6TGlWiDuwK64ra8Yg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ad380f15-f443-4098-a88b-70347fe6b4e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "40ec09a3-5ba7-49ba-8abb-a603da41f2a1", "AQAAAAEAACcQAAAAELmuZRgW/tVhvaL75aEqxv60VPvCdFqzRDJZQie8gSpGoTu0HXNRsyImaKJoBSKehw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b706a1dd-8e1b-4a26-b1f4-0681e82c59d3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a0f0dfe1-a020-4a85-988b-ceb3d85193f5", "AQAAAAEAACcQAAAAEObP+aspBzeZbxzlk+BXrQ9wkLJ6bWoDKN0lUTMnfHZ0vrlfgydHOQ26quZhiU+BKg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da3ff9e7-2647-457f-9b62-0ff9ab3177ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f98777f-4d74-4206-b459-1e8e409b0e56", "AQAAAAEAACcQAAAAEFuGV+60i+H0aiUAd0r0DzH3f7jeA9noe6jUDX2sYe6+XrtqztM488nQi/ndNFlq1g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3e0294ec-2b76-4ae8-ad4a-3b55e64fceeb"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f2291503-18bd-4adf-8572-7ba3f5b07405", "AQAAAAEAACcQAAAAENLe4z1kGMMxuPZ4JvCEg6sf+HEuFpokrBjPzTliko3B2Tw6PWLR/RC+ldEXIvfXrA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7cec0a00-a9b7-43a3-bfc4-f4778eb80f39"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc88a073-7694-448b-a1fa-bacd1b935ac0", "AQAAAAEAACcQAAAAEHpscc8RAb3PkukndsSkePVWI3pk5bwpf/UpBGu/BpFb4ugrMi5Td4Zez18ylqIMdg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ad380f15-f443-4098-a88b-70347fe6b4e5"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9ddbe302-62d1-4be7-9bdc-a0fdc2ae3571", "AQAAAAEAACcQAAAAEBTge+K4TqELFy7R1vmk/v1WyoKgfcOBQwTQypm/3bGFix7mqQ6R8s+/speQNCh0KA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b706a1dd-8e1b-4a26-b1f4-0681e82c59d3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2058064b-fce3-49df-83d1-7318d0a0d17e", "AQAAAAEAACcQAAAAEIBV0CX4X5iwH01inqx0oDOrVimHxpk9o6nb1ZCdpq+GhFHe1FycxcfjTC5brPlx7g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da3ff9e7-2647-457f-9b62-0ff9ab3177ca"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c06162c9-a083-4946-a201-b3fa0ecf700a", "AQAAAAEAACcQAAAAEIRa4ErAk711TPpEohIYXk6cGoQdMkhA0XnmZENc2dksyFXXI2zS+41lz4H3ZA/AhA==" });
        }
    }
}
