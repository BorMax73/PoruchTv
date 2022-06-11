using Microsoft.EntityFrameworkCore.Migrations;

namespace poruchTv.Migrations
{
    public partial class UpdateUserSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "UserSessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "UserSessions");
        }
    }
}
