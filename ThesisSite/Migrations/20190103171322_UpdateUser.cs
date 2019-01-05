using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEnrollments_Groups_GroupID",
                table: "GroupEnrollments");

            migrationBuilder.RenameColumn(
                name: "GroupID",
                table: "GroupEnrollments",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEnrollments_GroupID",
                table: "GroupEnrollments",
                newName: "IX_GroupEnrollments_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEnrollments_Groups_GroupId",
                table: "GroupEnrollments",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEnrollments_Groups_GroupId",
                table: "GroupEnrollments");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupEnrollments",
                newName: "GroupID");

            migrationBuilder.RenameIndex(
                name: "IX_GroupEnrollments_GroupId",
                table: "GroupEnrollments",
                newName: "IX_GroupEnrollments_GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupEnrollments_Groups_GroupID",
                table: "GroupEnrollments",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
