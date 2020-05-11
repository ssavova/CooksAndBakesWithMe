using Microsoft.EntityFrameworkCore.Migrations;

namespace CooksAndBakes.Data.Migrations
{
    public partial class AddNewFieldInRecipeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Recipes");
        }
    }
}
