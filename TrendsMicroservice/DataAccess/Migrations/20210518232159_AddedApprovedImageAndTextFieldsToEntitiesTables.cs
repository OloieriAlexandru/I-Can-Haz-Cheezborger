using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedApprovedImageAndTextFieldsToEntitiesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApprovedImage",
                table: "Trends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedText",
                table: "Trends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedImage",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedText",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedImage",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedText",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"),
                columns: new[] { "ApprovedImage", "ApprovedText" },
                values: new object[] { true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedImage",
                table: "Trends");

            migrationBuilder.DropColumn(
                name: "ApprovedText",
                table: "Trends");

            migrationBuilder.DropColumn(
                name: "ApprovedImage",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ApprovedText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ApprovedImage",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ApprovedText",
                table: "Comments");
        }
    }
}
