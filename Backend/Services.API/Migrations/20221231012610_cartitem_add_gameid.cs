using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.API.Migrations
{
    public partial class cartitem_add_gameid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "UserTokens",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "CartItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "CartItemDetails");
        }
    }
}
