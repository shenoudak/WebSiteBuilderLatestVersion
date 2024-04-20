using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iTech.Migrations
{
    /// <inheritdoc />
    public partial class SeddingAfffeedbackModelMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AffilateFeedbacks",
                columns: new[] { "AffilateFeedbackId", "TitleAr", "TitleEn", "UserOneFeedbackAr", "UserOneFeedbackEn", "UserThreeFeedbackAr", "UserThreeFeedbackEn", "UserTwoFeedbackAr", "UserTwoFeedbackEn" },
                values: new object[] { 1, "الاي تيك هي شركة النجاح", "Itech Is Successfull Company", "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", "Itech Help Me To Build My Live ....Eslam Ahmed", "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", "Itech Help Me To Build My Live ....Eslam Ahmed", "لقد ساعدتني الاي تيك كثرا في بناء حياتي ....اسلام احمد", "Itech Help Me To Build My Live ....Eslam Ahmed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AffilateFeedbacks",
                keyColumn: "AffilateFeedbackId",
                keyValue: 1);
        }
    }
}
