using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class UpdateFileUpload3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Topics_TopicId",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_TopicId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "FileUploads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "FileUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_TopicId",
                table: "FileUploads",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Topics_TopicId",
                table: "FileUploads",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
