using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class AddCategoriesToJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "job",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<List<string>>(
                name: "Categories",
                table: "job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "long_description",
                table: "job",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "job",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "job");

            migrationBuilder.DropColumn(
                name: "city",
                table: "job");

            migrationBuilder.DropColumn(
                name: "long_description",
                table: "job");

            migrationBuilder.DropColumn(
                name: "state",
                table: "job");

            migrationBuilder.AlterColumn<string>(
                name: "location",
                table: "job",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
