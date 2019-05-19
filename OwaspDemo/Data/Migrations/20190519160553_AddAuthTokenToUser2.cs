using Microsoft.EntityFrameworkCore.Migrations;

namespace OwaspDemo.Data.Migrations
{
    public partial class AddAuthTokenToUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthToken",
                table: "Logins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthToken",
                table: "Logins");
        }
    }
}
