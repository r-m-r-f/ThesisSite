using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class AddNavigationPropeties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimestamp",
                table: "TopicToStudents",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTimestamp",
                table: "TopicToStudents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TopicToStudents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimestamp",
                table: "FileUploads",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTimestamp",
                table: "FileUploads",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FileUploads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedTimestamp",
                table: "AssignmetsToStudent",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedTimestamp",
                table: "AssignmetsToStudent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "TopicToStudents");

            migrationBuilder.DropColumn(
                name: "DeletedTimestamp",
                table: "TopicToStudents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TopicToStudents");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "DeletedTimestamp",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "CreatedTimestamp",
                table: "AssignmetsToStudent");

            migrationBuilder.DropColumn(
                name: "DeletedTimestamp",
                table: "AssignmetsToStudent");
        }
    }
}
