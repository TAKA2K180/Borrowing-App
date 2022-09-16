using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Borrowing_App.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                schema: "Identity",
                table: "Borrower",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                schema: "Identity",
                table: "Borrower",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionId",
                schema: "Identity",
                table: "Borrower");

            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "Identity",
                table: "Borrower");
        }
    }
}
