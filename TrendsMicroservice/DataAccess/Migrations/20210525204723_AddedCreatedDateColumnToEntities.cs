using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddedCreatedDateColumnToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Trends",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("2976abf0-3cdb-4753-b48c-d37b144c6434"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(6010));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("44610d77-1627-47ec-a6d7-03bdd3d83c32"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(5964));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("72609609-31a0-42cc-92d1-c60ac13e0e37"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("9dd83f1d-77da-441e-9749-73eb8d88d5fc"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(5991));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("b359535d-0fcd-49c7-8647-58aae84fa456"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(6172));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("e9e2f1a2-0534-4892-a0c4-9a5718eec16b"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 251, DateTimeKind.Local).AddTicks(5981));

            migrationBuilder.UpdateData(
                table: "Trends",
                keyColumn: "Id",
                keyValue: new Guid("fa682095-d421-49f7-b6ed-cdf0aa68ba8a"),
                column: "CreateDate",
                value: new DateTime(2021, 5, 25, 23, 47, 23, 248, DateTimeKind.Local).AddTicks(2182));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Trends");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Comments");
        }
    }
}
