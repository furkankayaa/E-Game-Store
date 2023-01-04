using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.API.Migrations
{
    public partial class librarymtom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "LibraryDetails");

            migrationBuilder.RenameColumn(
                name: "GameApk",
                table: "GameDetails",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "GameApkName",
                table: "GameDetails",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GameDetailLibraryDetail",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "int", nullable: false),
                    LibrariesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetailLibraryDetail", x => new { x.GamesID, x.LibrariesID });
                    table.ForeignKey(
                        name: "FK_GameDetailLibraryDetail_GameDetails_GamesID",
                        column: x => x.GamesID,
                        principalTable: "GameDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDetailLibraryDetail_LibraryDetails_LibrariesID",
                        column: x => x.LibrariesID,
                        principalTable: "LibraryDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetailLibraryDetail_LibrariesID",
                table: "GameDetailLibraryDetail",
                column: "LibrariesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDetailLibraryDetail");

            migrationBuilder.DropColumn(
                name: "GameApkName",
                table: "GameDetails");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "GameDetails",
                newName: "GameApk");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "LibraryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
