using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class UpdateAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UploadLimit",
                table: "Assignments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "UploadLimit",
                table: "Assignments");
        }
    }
}
