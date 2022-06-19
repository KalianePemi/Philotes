using Microsoft.EntityFrameworkCore.Migrations;

namespace Philotes.Migrations
{
    public partial class UltimoLocalVisto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UltimoLocalVisto",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UltimoLocalVisto",
                table: "Pets");
        }
    }
}
