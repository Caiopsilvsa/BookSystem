using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSystem.Migrations
{
    /// <inheritdoc />
    public partial class Script2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AutorID",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Categoria_CategoriaID",
                table: "Livro");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Livro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AutorID",
                table: "Livro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AutorID",
                table: "Livro",
                column: "AutorID",
                principalTable: "Autor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Categoria_CategoriaID",
                table: "Livro",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Autor_AutorID",
                table: "Livro");

            migrationBuilder.DropForeignKey(
                name: "FK_Livro_Categoria_CategoriaID",
                table: "Livro");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "Livro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AutorID",
                table: "Livro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Autor_AutorID",
                table: "Livro",
                column: "AutorID",
                principalTable: "Autor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livro_Categoria_CategoriaID",
                table: "Livro",
                column: "CategoriaID",
                principalTable: "Categoria",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
