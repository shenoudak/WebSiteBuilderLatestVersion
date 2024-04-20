using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class AfffeedbackModelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffilateFeedbacks",
                columns: table => new
                {
                    AffilateFeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserOneFeedbackAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserOneFeedbackEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTwoFeedbackEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserTwoFeedbackAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserThreeFeedbackEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserThreeFeedbackAr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffilateFeedbacks", x => x.AffilateFeedbackId);
                });

                   }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffilateFeedbacks");

           
        }
    }
}
