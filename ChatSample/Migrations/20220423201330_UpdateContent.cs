using Microsoft.EntityFrameworkCore.Migrations;

namespace poruchTv.Migrations
{
    public partial class UpdateContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "popularity",
                table: "contents",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "genre_ids",
                table: "contents",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "vote_average",
                table: "contents",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genre_ids",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "vote_average",
                table: "contents");

            migrationBuilder.AlterColumn<string>(
                name: "popularity",
                table: "contents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
