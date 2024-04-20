using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class webEditor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebEditorPlans",
                columns: table => new
                {
                    WebEditorPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PriceAfterDiscount = table.Column<double>(type: "float", nullable: false),
                    RequestsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebEditorPlans", x => x.WebEditorPlanId);
                });

            migrationBuilder.CreateTable(
                name: "WebEditorSubscriptions",
                columns: table => new
                {
                    WESubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebEditorSubscriptions", x => x.WESubscriptionId);
                    table.ForeignKey(
                        name: "FK_WebEditorSubscriptions_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebEditorSubscriptions_WebEditorPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "WebEditorPlans",
                        principalColumn: "WebEditorPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteModificationRequests",
                columns: table => new
                {
                    SMReqId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    DesignerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModificationDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WESubscriptionId = table.Column<int>(type: "int", nullable: true),
                    WESubscriptionsWESubscriptionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteModificationRequests", x => x.SMReqId);
                    table.ForeignKey(
                        name: "FK_SiteModificationRequests_WebEditorSubscriptions_WESubscriptionsWESubscriptionId",
                        column: x => x.WESubscriptionsWESubscriptionId,
                        principalTable: "WebEditorSubscriptions",
                        principalColumn: "WESubscriptionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteModificationRequests_WESubscriptionsWESubscriptionId",
                table: "SiteModificationRequests",
                column: "WESubscriptionsWESubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_WebEditorSubscriptions_PlanId",
                table: "WebEditorSubscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WebEditorSubscriptions_SiteId",
                table: "WebEditorSubscriptions",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteModificationRequests");

            migrationBuilder.DropTable(
                name: "WebEditorSubscriptions");

            migrationBuilder.DropTable(
                name: "WebEditorPlans");
        }
    }
}
