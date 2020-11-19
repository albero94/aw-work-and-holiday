using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsLibrary.Migrations
{
    public partial class SeedIdentityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d3c998ce-99a3-4b08-a1b5-34fd8adafdc0", "831fe7dc-586a-4fce-b317-b9dbfeb8d533", "Admin", "ADMIN" },
                    { "15dfb174-bcb9-4910-96eb-43a21bd24e5b", "ec6807f3-d045-4647-a9a9-104f5cfe5d65", "Company", "COMPANY" },
                    { "3c75c911-c964-4c85-8b48-139168782868", "a8068ea8-8249-40a1-b553-924300ba7982", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "Id",
                keyValue: "15dfb174-bcb9-4910-96eb-43a21bd24e5b");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "Id",
                keyValue: "3c75c911-c964-4c85-8b48-139168782868");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "Id",
                keyValue: "d3c998ce-99a3-4b08-a1b5-34fd8adafdc0");
        }
    }
}
