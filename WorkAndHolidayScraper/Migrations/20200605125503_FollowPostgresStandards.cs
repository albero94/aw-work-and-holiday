using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkAndHolidayScraper.Migrations
{
    public partial class FollowPostgresStandards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: new Guid("c8bb9395-e774-4491-905d-e655e67a9d1d"));

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Jobs");

            migrationBuilder.RenameTable(
                name: "Jobs",
                newName: "job");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "job",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "job",
                newName: "salary");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "job",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Href",
                table: "job",
                newName: "href");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "job",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "job",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "job",
                newName: "company");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "job",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OriginalWebsite",
                table: "job",
                newName: "original_website");

            migrationBuilder.AddPrimaryKey(
                name: "PK_job",
                table: "job",
                column: "id");

            migrationBuilder.InsertData(
                table: "job",
                columns: new[] { "id", "company", "date", "description", "href", "location", "original_website", "salary", "title" },
                values: new object[] { new Guid("9169a369-dc68-45ea-a6cd-c56318d1bd44"), "Test company", new DateTime(2020, 6, 5, 8, 55, 2, 359, DateTimeKind.Local).AddTicks(505), "Test description", "www.google.com", "Australia", "Test", "a lot :)", "Test title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_job",
                table: "job");

            migrationBuilder.DeleteData(
                table: "job",
                keyColumn: "id",
                keyValue: new Guid("9169a369-dc68-45ea-a6cd-c56318d1bd44"));

            migrationBuilder.RenameTable(
                name: "job",
                newName: "Jobs");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Jobs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "salary",
                table: "Jobs",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Jobs",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "href",
                table: "Jobs",
                newName: "Href");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Jobs",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Jobs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "company",
                table: "Jobs",
                newName: "Company");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Jobs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "original_website",
                table: "Jobs",
                newName: "OriginalWebsite");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jobs",
                table: "Jobs",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Company", "Date", "Description", "Href", "Location", "OriginalWebsite", "Salary", "Title", "Type" },
                values: new object[] { new Guid("c8bb9395-e774-4491-905d-e655e67a9d1d"), "Test company", new DateTime(2020, 6, 3, 21, 33, 12, 456, DateTimeKind.Local).AddTicks(4679), "Test description", "www.google.com", "Australia", "Test", "a lot :)", "Test title", null });
        }
    }
}
