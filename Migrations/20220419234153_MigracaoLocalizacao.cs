using Microsoft.EntityFrameworkCore.Migrations;

namespace Philotes.Migrations
{
    public partial class MigracaoLocalizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Localizacao_LocalizacaoId",
                table: "Eventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao");

            migrationBuilder.RenameTable(
                name: "Localizacao",
                newName: "Enderecos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Enderecos_LocalizacaoId",
                table: "Eventos",
                column: "LocalizacaoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Enderecos_LocalizacaoId",
                table: "Eventos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Localizacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localizacao",
                table: "Localizacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Localizacao_LocalizacaoId",
                table: "Eventos",
                column: "LocalizacaoId",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
