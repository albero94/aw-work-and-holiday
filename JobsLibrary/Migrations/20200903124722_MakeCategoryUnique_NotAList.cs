using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JobsLibrary.Migrations
{
    public partial class MakeCategoryUnique_NotAList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "job");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "job",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "job_category",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_category", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_job_category_CategoryId",
                table: "job");

            migrationBuilder.DropTable(
                name: "job_category");

            migrationBuilder.DropIndex(
                name: "IX_job_CategoryId",
                table: "job");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "job");

            migrationBuilder.AddColumn<string[]>(
                name: "Categories",
                table: "job",
                type: "text[]",
                nullable: true);
        }
    }
}
