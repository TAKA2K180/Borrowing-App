using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrowing_App.Migrations
{
    public partial class _2ndauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                schema: "Identity",
                table: "Borrower");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                schema: "Identity",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                schema: "Identity",
                table: "Borrower",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
