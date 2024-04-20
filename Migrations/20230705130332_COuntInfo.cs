using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class COuntInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CountInfoId",
                table: "Count",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "countsCountId",
                table: "Count",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CountInfos",
                columns: table => new
                {
                    CountInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescAR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountInfos", x => x.CountInfoId);
                });

            migrationBuilder.InsertData(
                table: "CountInfos",
                columns: new[] { "CountInfoId", "DescAR", "DescEN", "TitleAR", "TitleEN" },
                values: new object[] { 1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc.", "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus blandit massa enim. Nullam id varius nunc.", "  أرقام الشركة", "Company in Numbers" });

            migrationBuilder.CreateIndex(
                name: "IX_Count_CountInfoId",
                table: "Count",
                column: "CountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Count_countsCountId",
                table: "Count",
                column: "countsCountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Count_CountInfos_CountInfoId",
                table: "Count",
                column: "CountInfoId",
                principalTable: "CountInfos",
                principalColumn: "CountInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Count_Count_countsCountId",
                table: "Count",
                column: "countsCountId",
                principalTable: "Count",
                principalColumn: "CountId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Count_CountInfos_CountInfoId",
                table: "Count");

            migrationBuilder.DropForeignKey(
                name: "FK_Count_Count_countsCountId",
                table: "Count");

            migrationBuilder.DropTable(
                name: "CountInfos");

            migrationBuilder.DropIndex(
                name: "IX_Count_CountInfoId",
                table: "Count");

            migrationBuilder.DropIndex(
                name: "IX_Count_countsCountId",
                table: "Count");

            migrationBuilder.DropColumn(
                name: "CountInfoId",
                table: "Count");

            migrationBuilder.DropColumn(
                name: "countsCountId",
                table: "Count");

            migrationBuilder.InsertData(
                table: "Count",
                columns: new[] { "CountId", "TitleAR", "TitleEN", "count" },
                values: new object[,]
                {
                    { 1, "مستشارون محترفون", "Pro Consultants", 300 },
                    { 2, "عملاء راضون", "Satisfied Clients", 200 },
                    { 3, "حالات ناجحة", "Sucessfull Cases", 100 },
                    { 4, "سنوات في الأعمال", "Years in Business", 20 }
                });
        }
    }
}
