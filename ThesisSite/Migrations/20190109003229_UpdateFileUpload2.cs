using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class UpdateFileUpload2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Topics_TopicId",
                table: "FileUploads"
                );

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_TopicId",
                table: "FileUploads");
        }
    }
}
