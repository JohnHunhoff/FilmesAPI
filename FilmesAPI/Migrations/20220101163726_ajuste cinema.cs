using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class ajustecinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoId",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "Cinemas",
                newName: "EnderecoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Cinemas_EnderecoId",
                table: "Cinemas",
                newName: "IX_Cinemas_EnderecoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoFK",
                table: "Cinemas",
                column: "EnderecoFK",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoFK",
                table: "Cinemas");

            migrationBuilder.RenameColumn(
                name: "EnderecoFK",
                table: "Cinemas",
                newName: "EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_Cinemas_EnderecoFK",
                table: "Cinemas",
                newName: "IX_Cinemas_EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Enderecos_EnderecoId",
                table: "Cinemas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
