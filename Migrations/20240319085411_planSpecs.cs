using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class planSpecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlansSpecs",
                columns: table => new
                {
                    PlanSpecsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlansSpecs", x => x.PlanSpecsId);
                    table.ForeignKey(
                        name: "FK_PlansSpecs_WebEditorPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "WebEditorPlans",
                        principalColumn: "WebEditorPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlansSpecs_PlanId",
                table: "PlansSpecs",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlansSpecs");
        }
    }
}
