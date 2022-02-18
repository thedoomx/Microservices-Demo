using Microsoft.EntityFrameworkCore.Migrations;

namespace Oxygen.Company.Infrastructure.Persistence.Migrations
{
    public partial class AddUserIdToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");
        }
    }
}
