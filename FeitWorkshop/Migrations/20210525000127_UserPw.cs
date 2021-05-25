using Microsoft.EntityFrameworkCore.Migrations;

namespace FeitWorkshop.Migrations
{
    public partial class UserPw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Teacher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Teacher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Student");
        }
    }
}
