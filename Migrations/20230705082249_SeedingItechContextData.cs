using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class SeedingItechContextData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BrandsBrief",
                columns: new[] { "BrandsBriefId", "BriefAR", "BriefEN" },
                values: new object[] { 1, "يستخدم 45000 عميل في 100 دولة نموذج ITECH. قابل عملائنا.", "45,000 CUSTOMERS IN 100 COUNTRIES USE ITECH TEMPLATE. MEET OUR CUSTOMERS." });

            migrationBuilder.InsertData(
                table: "PageContents",
                columns: new[] { "PageContentId", "ContentAr", "ContentEn", "PageTitleAr", "PageTitleEn" },
                values: new object[,]
                {
                    { 1, "Privacy", "Privacy", "سياسة الخصوصية", "Privacy" },
                    { 2, "AboutUs", "AboutUs", "من نحن", "AboutUs" },
                    { 3, "Terms", "Terms", "الشروط والأحكام", "Terms" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypeId", "TitleAr", "TitleEn" },
                values: new object[,]
                {
                    { 1, "كاش", "Cash" },
                    { 2, "اونلاين", "Online" }
                });

            migrationBuilder.InsertData(
                table: "RequestStatuses",
                columns: new[] { "RequestStatusId", "StatusTitleAr", "StatusTitleEn" },
                values: new object[,]
                {
                    { 1, "تم انشاءالطلب", "Initiated" },
                    { 2, "تم رفض الطلب", "Rejected" },
                    { 3, "عملية الدفع تمت بنجاح", "Paid" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BrandsBrief",
                keyColumn: "BrandsBriefId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PageContents",
                keyColumn: "PageContentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "RequestStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "RequestStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequestStatuses",
                keyColumn: "RequestStatusId",
                keyValue: 3);
        }
    }
}
