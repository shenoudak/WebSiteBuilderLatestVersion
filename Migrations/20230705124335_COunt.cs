using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class COunt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Count",
                columns: table => new
                {
                    CountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    count = table.Column<int>(type: "int", nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleAR = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Count", x => x.CountId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Count");
        }
    }
}
