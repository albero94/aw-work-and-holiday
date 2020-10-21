using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class AddSalyIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "salary_id",
                table: "job",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salary_id",
                table: "job");
        }
    }
}
