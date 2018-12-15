using Microsoft.EntityFrameworkCore.Migrations;

namespace ThesisSite.Migrations
{
    public partial class AddShortNameInCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
