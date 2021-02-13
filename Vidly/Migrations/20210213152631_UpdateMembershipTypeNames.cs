using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class UpdateMembershipTypeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Pay as You Go' WHERE DurationInMonths = 0");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE DurationInMonths = 1");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE DurationInMonths = 3");
            migrationBuilder.Sql("UPDATE MembershipTypes SET Name = 'Annual' WHERE DurationInMonths = 12");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
