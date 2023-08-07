using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSystem.Migrations
{
    /// <inheritdoc />
    public partial class CampoFotoadicionado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Livro",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Livro");
        }
    }
}
