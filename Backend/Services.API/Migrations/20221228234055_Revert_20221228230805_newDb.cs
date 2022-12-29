using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.API.Migrations
{
    public partial class Revert_20221228230805_newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenreLinks_GameDetails_GameId",
                table: "GameGenreLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenreLinks_GenreDetails_GenreId",
                table: "GameGenreLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrderLinks_OrderDetails_OrderId",
                table: "GameOrderLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrderLinks_OrderedGamesDetails_GameId",
                table: "GameOrderLinks");

            migrationBuilder.DropTable(
                name: "GameDetailGenreDetail");

            migrationBuilder.DropTable(
                name: "OrderDetailOrderedGamesDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameOrderLinks",
                table: "GameOrderLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenreLinks",
                table: "GameGenreLinks");

            migrationBuilder.RenameTable(
                name: "GameOrderLinks",
                newName: "GameOrderLink");

            migrationBuilder.RenameTable(
                name: "GameGenreLinks",
                newName: "GameGenreLink");

            migrationBuilder.RenameIndex(
                name: "IX_GameOrderLinks_OrderId",
                table: "GameOrderLink",
                newName: "IX_GameOrderLink_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenreLinks_GenreId",
                table: "GameGenreLink",
                newName: "IX_GameGenreLink_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameOrderLink",
                table: "GameOrderLink",
                columns: new[] { "GameId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenreLink",
                table: "GameGenreLink",
                columns: new[] { "GameId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenreLink_GameDetails_GameId",
                table: "GameGenreLink",
                column: "GameId",
                principalTable: "GameDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenreLink_GenreDetails_GenreId",
                table: "GameGenreLink",
                column: "GenreId",
                principalTable: "GenreDetails",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrderLink_OrderDetails_OrderId",
                table: "GameOrderLink",
                column: "OrderId",
                principalTable: "OrderDetails",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrderLink_OrderedGamesDetails_GameId",
                table: "GameOrderLink",
                column: "GameId",
                principalTable: "OrderedGamesDetails",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenreLink_GameDetails_GameId",
                table: "GameGenreLink");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenreLink_GenreDetails_GenreId",
                table: "GameGenreLink");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrderLink_OrderDetails_OrderId",
                table: "GameOrderLink");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOrderLink_OrderedGamesDetails_GameId",
                table: "GameOrderLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameOrderLink",
                table: "GameOrderLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenreLink",
                table: "GameGenreLink");

            migrationBuilder.RenameTable(
                name: "GameOrderLink",
                newName: "GameOrderLinks");

            migrationBuilder.RenameTable(
                name: "GameGenreLink",
                newName: "GameGenreLinks");

            migrationBuilder.RenameIndex(
                name: "IX_GameOrderLink_OrderId",
                table: "GameOrderLinks",
                newName: "IX_GameOrderLinks_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenreLink_GenreId",
                table: "GameGenreLinks",
                newName: "IX_GameGenreLinks_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameOrderLinks",
                table: "GameOrderLinks",
                columns: new[] { "GameId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenreLinks",
                table: "GameGenreLinks",
                columns: new[] { "GameId", "GenreId" });

            migrationBuilder.CreateTable(
                name: "GameDetailGenreDetail",
                columns: table => new
                {
                    GamesID = table.Column<int>(type: "int", nullable: false),
                    GenresGenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDetailGenreDetail", x => new { x.GamesID, x.GenresGenreID });
                    table.ForeignKey(
                        name: "FK_GameDetailGenreDetail_GameDetails_GamesID",
                        column: x => x.GamesID,
                        principalTable: "GameDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDetailGenreDetail_GenreDetails_GenresGenreID",
                        column: x => x.GenresGenreID,
                        principalTable: "GenreDetails",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderDetailOrderedGamesDetail",
                columns: table => new
                {
                    OrderedGamesGameId = table.Column<int>(type: "int", nullable: false),
                    OrdersOrderNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailOrderedGamesDetail", x => new { x.OrderedGamesGameId, x.OrdersOrderNum });
                    table.ForeignKey(
                        name: "FK_OrderDetailOrderedGamesDetail_OrderDetails_OrdersOrderNum",
                        column: x => x.OrdersOrderNum,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderNum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailOrderedGamesDetail_OrderedGamesDetails_OrderedGam~",
                        column: x => x.OrderedGamesGameId,
                        principalTable: "OrderedGamesDetails",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetailGenreDetail_GenresGenreID",
                table: "GameDetailGenreDetail",
                column: "GenresGenreID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailOrderedGamesDetail_OrdersOrderNum",
                table: "OrderDetailOrderedGamesDetail",
                column: "OrdersOrderNum");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenreLinks_GameDetails_GameId",
                table: "GameGenreLinks",
                column: "GameId",
                principalTable: "GameDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenreLinks_GenreDetails_GenreId",
                table: "GameGenreLinks",
                column: "GenreId",
                principalTable: "GenreDetails",
                principalColumn: "GenreID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrderLinks_OrderDetails_OrderId",
                table: "GameOrderLinks",
                column: "OrderId",
                principalTable: "OrderDetails",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOrderLinks_OrderedGamesDetails_GameId",
                table: "GameOrderLinks",
                column: "GameId",
                principalTable: "OrderedGamesDetails",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
