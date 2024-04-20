using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class CountDownSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountDownSections",
                columns: table => new
                {
                    CountDownSectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextAR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountDownSections", x => x.CountDownSectionId);
                });

            migrationBuilder.InsertData(
                table: "CountDownSections",
                columns: new[] { "CountDownSectionId", "TextAR", "TextEN" },
                values: new object[] { 1, "قسم العد التنازلي", "countdown section" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountDownSections");
        }
    }
}
