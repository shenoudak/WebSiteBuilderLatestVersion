using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class webEditor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d20bf37-8151-455c-8d98-20dc552fc98e", null, "Affiliate", "AFFILIATE" },
                    { "445f816e-354d-48bc-beb1-78223c69b86c", null, "WebEditor", "WEBEDITOR" },
                    { "c73d00ed-0b74-4be4-875c-262669adf851", null, "Admin", "ADMIN" },
                    { "d4a9d25f-afbe-44bf-b0e2-9205b153227d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d20bf37-8151-455c-8d98-20dc552fc98e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "445f816e-354d-48bc-beb1-78223c69b86c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c73d00ed-0b74-4be4-875c-262669adf851");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4a9d25f-afbe-44bf-b0e2-9205b153227d");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

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
    }
}
