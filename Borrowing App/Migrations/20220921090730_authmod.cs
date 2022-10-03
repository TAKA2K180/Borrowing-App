using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrowing_App.Migrations
{
    public partial class authmod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Identity",
                table: "Borrower",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                schema: "Identity",
                table: "Borrower",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                schema: "Identity",
                table: "Borrower");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Identity",
                table: "Borrower",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
