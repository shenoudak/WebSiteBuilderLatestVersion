using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class seddingPublicVideoModelAfterUpdateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1,
                columns: new[] { "Background", "VideoUrl" },
                values: new object[] { "public/img/custom-header-bg.jpg", "https://vimeo.com/45830194" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1,
                columns: new[] { "Background", "VideoUrl" },
                values: new object[] { "", "" });
        }
    }
}
