using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Philotes.Migrations
{
    public partial class MigracaoUsuarioDataAcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataAcesso",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAcesso",
                table: "Usuarios");
        }
    }
}
