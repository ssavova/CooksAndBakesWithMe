using Microsoft.EntityFrameworkCore.Migrations;

namespace CooksAndBakes.Data.Migrations
{
    public partial class NewColomnInRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Recipes");
        }
    }
}
