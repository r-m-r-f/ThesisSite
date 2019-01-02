using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class AddAssignmentDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Assignments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Assignments");
        }
    }
}
