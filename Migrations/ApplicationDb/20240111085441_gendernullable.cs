using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class gendernullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "434f4e7c-558d-4df4-bfb5-5ff38de38d9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b252923a-911e-4e06-bde7-469bccd4afa4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d93fb908-1750-4ae8-9b63-4eb7b389d667");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23487fec-f9c5-4410-91eb-48e5b3b721c1", null, "User", "USER" },
                    { "8c871a68-42bf-4ef9-99c6-896b539547fe", null, "Admin", "ADMIN" },
                    { "cc17ee3a-bc4d-46f7-b775-4fc050128df2", null, "Affiliate", "AFFILIATE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23487fec-f9c5-4410-91eb-48e5b3b721c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c871a68-42bf-4ef9-99c6-896b539547fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc17ee3a-bc4d-46f7-b775-4fc050128df2");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "434f4e7c-558d-4df4-bfb5-5ff38de38d9b", null, "Affiliate", "AFFILIATE" },
                    { "b252923a-911e-4e06-bde7-469bccd4afa4", null, "Admin", "ADMIN" },
                    { "d93fb908-1750-4ae8-9b63-4eb7b389d667", null, "User", "USER" }
                });
        }
    }
}
