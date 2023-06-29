using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noticias.Migrations
{
    /// <inheritdoc />
    public partial class migration_05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeCategoria",
                table: "Noticias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCategoria",
                table: "Noticias");
        }
    }
}
