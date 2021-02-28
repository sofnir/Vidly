using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class PopulateGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Genres", "Name", new object[] {
                "Comedy",
                "Action",
                "Family",
                "Romance"
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Genres", "Name", new object[] {
                "Comedy",
                "Action",
                "Family",
                "Romance"
            });
        }
    }
}
