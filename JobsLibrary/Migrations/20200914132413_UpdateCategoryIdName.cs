using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class UpdateCategoryIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_job_category_CategoryId",
                table: "job");

            migrationBuilder.DropIndex(
                name: "IX_job_CategoryId",
                table: "job");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "job",
                newName: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "job",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_job_CategoryId",
                table: "job",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_job_job_category_CategoryId",
                table: "job",
                column: "CategoryId",
                principalTable: "job_category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
