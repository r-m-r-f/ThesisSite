using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class AddTopicToStudentsNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "TopicToStudents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicToStudents_AssignmentId",
                table: "TopicToStudents",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicToStudents_Assignments_AssignmentId",
                table: "TopicToStudents",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicToStudents_Assignments_AssignmentId",
                table: "TopicToStudents");

            migrationBuilder.DropIndex(
                name: "IX_TopicToStudents_AssignmentId",
                table: "TopicToStudents");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "TopicToStudents");
        }
    }
}
