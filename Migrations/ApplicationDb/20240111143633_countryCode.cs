using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class countryCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33ace406-446f-483b-b444-7344b37cb41b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58791fec-8c8d-4ed3-9329-c8db37e8f87c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e153483-219c-4009-9ec7-61f3fd832010");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bf80a36-a225-42f9-be7d-1ee6724a48a8", null, "User", "USER" },
                    { "9c1cde31-e8b6-456a-8b5e-6c0246df48bb", null, "Affiliate", "AFFILIATE" },
                    { "ab515235-844b-4aa8-ab74-42d0f999f175", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bf80a36-a225-42f9-be7d-1ee6724a48a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c1cde31-e8b6-456a-8b5e-6c0246df48bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab515235-844b-4aa8-ab74-42d0f999f175");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33ace406-446f-483b-b444-7344b37cb41b", null, "Affiliate", "AFFILIATE" },
                    { "58791fec-8c8d-4ed3-9329-c8db37e8f87c", null, "User", "USER" },
                    { "7e153483-219c-4009-9ec7-61f3fd832010", null, "Admin", "ADMIN" }
                });
        }
    }
}
