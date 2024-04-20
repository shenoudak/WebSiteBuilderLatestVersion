using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class seddingPublicVideoModelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PublicVideos",
                columns: new[] { "PublicVideoId", "BriefAr", "BriefEn", "TitleAr", "TitleEn" },
                values: new object[] { 1, "عن طريق الاي تيك في اقل من خمس خطوات", "Through iTech in less than five steps", "إنشاء موقعك في دقائق", "Create your site in minutes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1);
        }
    }
}
