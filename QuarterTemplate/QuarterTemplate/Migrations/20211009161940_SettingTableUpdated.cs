using Microsoft.EntityFrameworkCore.Migrations;

namespace QuarterTemplate.Migrations
{
    public partial class SettingTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "EmailImage",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailLogo",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationImage",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationLogo",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainEmail",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainPhone",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneImage",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneLogo",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondEmail",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPhone",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailImage",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "EmailLogo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LocationImage",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LocationLogo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "MainEmail",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "MainPhone",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PhoneImage",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "PhoneLogo",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "SecondEmail",
                table: "Settings");

            migrationBuilder.DropColumn(    
                name: "SecondPhone",
                table: "Settings");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }
    }
}
