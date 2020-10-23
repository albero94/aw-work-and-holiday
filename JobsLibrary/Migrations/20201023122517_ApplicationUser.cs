using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "user",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_company",
                table: "user",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "is_company",
                table: "user");
        }
    }
}
