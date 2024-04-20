using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class updatePublicVideoModelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "PublicVideos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PublicVideos",
                keyColumn: "PublicVideoId",
                keyValue: 1,
                column: "VideoUrl",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "PublicVideos");
        }
    }
}
