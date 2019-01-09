using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class UpdateFileUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "FileUploads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Topic",
                table: "FileUploads",
                nullable: false,
                defaultValue: 0);
        }
    }
}
