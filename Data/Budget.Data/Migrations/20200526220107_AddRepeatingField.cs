using Microsoft.EntityFrameworkCore.Migrations;

namespace Budget.Data.Migrations
{
    public partial class AddRepeatingField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRepeating",
                table: "Incomes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeating",
                table: "Expenses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRepeating",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "IsRepeating",
                table: "Expenses");
        }
    }
}
