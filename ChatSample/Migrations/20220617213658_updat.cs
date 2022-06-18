using Microsoft.EntityFrameworkCore.Migrations;

namespace poruchTv.Migrations
{
    public partial class updat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    OriginalTitle = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Overview = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Popularity = table.Column<double>(nullable: false),
                    VoteAverage = table.Column<double>(nullable: false),
                    ReleaseDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentUrls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentInfoId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUrls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentInfos");

            migrationBuilder.DropTable(
                name: "ContentUrls");
        }
    }
}
