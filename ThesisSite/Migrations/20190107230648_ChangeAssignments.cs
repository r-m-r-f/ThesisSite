using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class ChangeAssignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FileUploads");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Groups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "GroupEnrollments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "FileUploads",
                newName: "AssignmentToStudentId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Courses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Assignments",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "AssignmetsToStudentId",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AssignmetsToStudent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DueTo",
                table: "Assignments",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Assignments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Assignments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedTimestamp = table.Column<DateTimeOffset>(nullable: false),
                    DeletedTimestamp = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AssignmentId = table.Column<int>(nullable: false),
                    Limit = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicToStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicToStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicToStudents_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicToStudents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_AssignmetsToStudentId",
                table: "FileUploads",
                column: "AssignmetsToStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AssignmentId",
                table: "Topics",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicToStudents_TopicId",
                table: "TopicToStudents",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicToStudents_UserId",
                table: "TopicToStudents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_AssignmetsToStudent_AssignmetsToStudentId",
                table: "FileUploads",
                column: "AssignmetsToStudentId",
                principalTable: "AssignmetsToStudent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_AssignmetsToStudent_AssignmetsToStudentId",
                table: "FileUploads");

            migrationBuilder.DropTable(
                name: "TopicToStudents");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_AssignmetsToStudentId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "AssignmetsToStudentId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AssignmetsToStudent");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GroupEnrollments",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AssignmentToStudentId",
                table: "FileUploads",
                newName: "AssignmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Courses",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assignments",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueTo",
                table: "Assignments",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
