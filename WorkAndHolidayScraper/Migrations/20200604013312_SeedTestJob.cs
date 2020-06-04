using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAndHolidayScraper.Migrations
{
    public partial class SeedTestJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalWebsite",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Company", "Date", "Description", "Href", "Location", "OriginalWebsite", "Salary", "Title", "Type" },
                values: new object[] { new Guid("c8bb9395-e774-4491-905d-e655e67a9d1d"), "Test company", new DateTime(2020, 6, 3, 21, 33, 12, 456, DateTimeKind.Local).AddTicks(4679), "Test description", "www.google.com", "Australia", "Test", "a lot :)", "Test title", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("c8bb9395-e774-4491-905d-e655e67a9d1d"));

            migrationBuilder.AlterColumn<string>(
                name: "OriginalWebsite",
                table: "Jobs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
