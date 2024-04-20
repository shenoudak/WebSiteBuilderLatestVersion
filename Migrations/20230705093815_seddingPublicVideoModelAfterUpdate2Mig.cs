using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class seddingPublicVideoModelAfterUpdate2Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1,
                column: "Background",
                value: "Public/img/generic-3.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1,
                column: "Background",
                value: "public/img/custom-header-bg.jpg");
        }
    }
}
