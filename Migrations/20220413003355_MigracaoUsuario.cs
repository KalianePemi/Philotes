using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Philotes.Migrations
{
    public partial class MigracaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoURL",
                table: "Pets");

            migrationBuilder.AddColumn<byte[]>(
                name: "FotoPet",
                table: "Pets",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UsuarioId",
                table: "Pets",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Usuarios_UsuarioId",
                table: "Pets",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Usuarios_UsuarioId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pets_UsuarioId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "FotoPet",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "FotoURL",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
