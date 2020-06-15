using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    company = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    href = table.Column<string>(nullable: true),
                    original_website = table.Column<string>(nullable: true),
                    salary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "job",
                columns: new[] { "id", "company", "date", "description", "href", "location", "original_website", "salary", "title" },
                values: new object[] { new Guid("396741ca-d70d-4903-b40e-6a337ee8691f"), "Test company", new DateTime(2020, 6, 15, 8, 56, 40, 578, DateTimeKind.Local).AddTicks(3946), "Test description", "www.google.com", "Australia", "Test", "a lot :)", "Test title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job");
        }
    }
}
