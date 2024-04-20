using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class sponsornullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "sponserName",
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
                    { "33ace406-446f-483b-b444-7344b37cb41b", null, "Affiliate", "AFFILIATE" },
                    { "58791fec-8c8d-4ed3-9329-c8db37e8f87c", null, "User", "USER" },
                    { "7e153483-219c-4009-9ec7-61f3fd832010", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "sponserName",
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
                    { "23487fec-f9c5-4410-91eb-48e5b3b721c1", null, "User", "USER" },
                    { "8c871a68-42bf-4ef9-99c6-896b539547fe", null, "Admin", "ADMIN" },
                    { "cc17ee3a-bc4d-46f7-b775-4fc050128df2", null, "Affiliate", "AFFILIATE" }
                });
        }
    }
}
