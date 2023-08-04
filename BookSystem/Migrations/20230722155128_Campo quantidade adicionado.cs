using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSystem.Migrations
{
    /// <inheritdoc />
    public partial class Campoquantidadeadicionado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Livro",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Livro");
        }
    }
}
