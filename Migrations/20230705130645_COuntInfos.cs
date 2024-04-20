using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class COuntInfos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Count_Count_countsCountId",
                table: "Count");

            migrationBuilder.DropIndex(
                name: "IX_Count_countsCountId",
                table: "Count");

            migrationBuilder.DropColumn(
                name: "countsCountId",
                table: "Count");

            migrationBuilder.InsertData(
                table: "Count",
                columns: new[] { "CountId", "CountInfoId", "TitleAR", "TitleEN", "count" },
                values: new object[,]
                {
                    { 1, 1, "مستشارون محترفون", "Pro Consultants", 300 },
                    { 2, 1, "عملاء راضون", "Satisfied Clients", 200 },
                    { 3, 1, "حالات ناجحة", "Sucessfull Cases", 100 },
                    { 4, 1, "سنوات في الأعمال", "Years in Business", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Count",
                keyColumn: "CountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Count",
                keyColumn: "CountId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Count",
                keyColumn: "CountId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Count",
                keyColumn: "CountId",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "countsCountId",
                table: "Count",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Count_countsCountId",
                table: "Count",
                column: "countsCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Count_Count_countsCountId",
                table: "Count",
                column: "countsCountId",
                principalTable: "Count",
                principalColumn: "CountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
