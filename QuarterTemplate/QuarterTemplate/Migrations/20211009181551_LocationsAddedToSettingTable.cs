using Microsoft.EntityFrameworkCore.Migrations;

namespace QuarterTemplate.Migrations
{
    public partial class LocationsAddedToSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location1",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location2",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location3",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location1",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Location2",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Location3",
                table: "Settings");
        }
    }
}
