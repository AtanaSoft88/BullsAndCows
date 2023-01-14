using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretNumberGame.Data.Migrations
{
    public partial class game_secretNumber_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecretNumber",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecretNumber",
                table: "Games");
        }
    }
}
