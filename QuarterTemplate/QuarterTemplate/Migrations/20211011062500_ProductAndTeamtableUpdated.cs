using Microsoft.EntityFrameworkCore.Migrations;

namespace QuarterTemplate.Migrations
{
    public partial class ProductAndTeamtableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_TeamId",
                table: "Products",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Teams_TeamId",
                table: "Products",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Teams_TeamId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TeamId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Products");
        }
    }
}
