using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class addSubscriptionModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "SiteSubscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SiteSubscriptions_PlanId",
                table: "SiteSubscriptions",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SiteSubscriptions_Plans_PlanId",
                table: "SiteSubscriptions",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SiteSubscriptions_Plans_PlanId",
                table: "SiteSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_SiteSubscriptions_PlanId",
                table: "SiteSubscriptions");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "SiteSubscriptions");
        }
    }
}
