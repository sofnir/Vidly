using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class PopulateMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var table = "MembershipType";
            var columns = new[] { "Id", "SignUpFee", "DurationInMonths", "DiscountRate" };

            migrationBuilder.InsertData(table, columns, new object[] { 1, 0, 0, 0 });
            migrationBuilder.InsertData(table, columns, new object[] { 2, 30, 1, 10 });
            migrationBuilder.InsertData(table, columns, new object[] { 3, 90, 3, 15 });
            migrationBuilder.InsertData(table, columns, new object[] { 4, 300, 12, 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
