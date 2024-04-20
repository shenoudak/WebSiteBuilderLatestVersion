using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class seedingPlanModelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "PagesCount", "Price", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, 3, 120.0, "ألاساسة", "BASIC" },
                    { 2, 5, 150.0, "القياسية", "Standard" },
                    { 3, 100, 190.0, "غير محدودة", "Enterprise" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 3);
        }
    }
}
