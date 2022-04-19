using Microsoft.EntityFrameworkCore.Migrations;

namespace Philotes.Migrations
{
    public partial class MigracaoPetCor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Pets");

            migrationBuilder.CreateTable(
                name: "Cor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetCores",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false),
                    CorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetCores", x => new { x.PetId, x.CorId });
                    table.ForeignKey(
                        name: "FK_PetCores_Cor_CorId",
                        column: x => x.CorId,
                        principalTable: "Cor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetCores_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cor",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Branco" });

            migrationBuilder.InsertData(
                table: "PetCores",
                columns: new[] { "CorId", "PetId" },
                values: new object[] { 1, 5 });

            migrationBuilder.CreateIndex(
                name: "IX_PetCores_CorId",
                table: "PetCores",
                column: "CorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetCores");

            migrationBuilder.DropTable(
                name: "Cor");

            migrationBuilder.AddColumn<int>(
                name: "Cor",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 5,
                column: "Cor",
                value: 7);
        }
    }
}
