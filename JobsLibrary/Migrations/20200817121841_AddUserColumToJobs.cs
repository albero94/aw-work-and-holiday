using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class AddUserColumToJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "job",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_job_UserId",
                table: "job",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_job_user_UserId",
                table: "job",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_user_UserId",
                table: "job");

            migrationBuilder.DropIndex(
                name: "IX_job_UserId",
                table: "job");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "job");
        }
    }
}
