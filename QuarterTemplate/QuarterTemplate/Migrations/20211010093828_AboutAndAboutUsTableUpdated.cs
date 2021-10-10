using Microsoft.EntityFrameworkCore.Migrations;

namespace QuarterTemplate.Migrations
{
    public partial class AboutAndAboutUsTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AboutUsId",
                table: "Abouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Abouts_AboutUsId",
                table: "Abouts",
                column: "AboutUsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abouts_AboutUses_AboutUsId",
                table: "Abouts",
                column: "AboutUsId",
                principalTable: "AboutUses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abouts_AboutUses_AboutUsId",
                table: "Abouts");

            migrationBuilder.DropIndex(
                name: "IX_Abouts_AboutUsId",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "AboutUsId",
                table: "Abouts");
        }
    }
}
