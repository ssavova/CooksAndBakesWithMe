using Microsoft.EntityFrameworkCore.Migrations;

namespace CooksAndBakes.Data.Migrations
{
    public partial class RemoveColomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
